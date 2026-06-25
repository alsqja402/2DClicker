using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static BackGroundCtlr;

public class Session : MonoBehaviour
{
    [Header("컴포넌트")]
    [SerializeField] Hero _hero;
    [SerializeField] Upgrader _upgrader;
    [SerializeField] SessionData _data;
    [SerializeField] RebirthSessionData _rebirthData;

    [Header("뷰")]
    [SerializeField] View _view;
    [SerializeField] UpgraderView _upgraderView;
    [SerializeField] EnemyView _enemyView;
    [SerializeField] BackGroundCtlr _backGroundCtlr;
    [SerializeField] StageTransitionView _stageTransitionView;

    [Header("적")]
    [SerializeField] Enemy _enemy;
    [SerializeField] Enemy[] _enemyPrefabs;
    [SerializeField] Transform _enemyParent;

    [Header("동료")]
    [SerializeField] AllyCtrl _allyCtrl;

    [Header("보스")]
    [SerializeField] Boss _boss;
    [SerializeField] Boss[] _bossPrefabs;
    [SerializeField] Transform _bossParent;
    [SerializeField] float _bossLimitTime = 30f;

    float _bossRemainTime;
    Coroutine _bossTimerCoroutine;
    bool _isBossBattleActive;

    [Header("이펙트")]
    [SerializeField] DamageSpawner _damageSpawner;
    [SerializeField] GoldSpawner _goldSpawner;
    [SerializeField] Particle _particle;
    [SerializeField] Rebirth _rebirth;

    [Header("장비")]
    [SerializeField] EquipmentDropTable _equipmentDropTable;
    [SerializeField] float _equipmentRewardChance = 30f;
    [SerializeField] EquipmentRewardView _equipmentRewardView;
    [SerializeField] EquipmentManager _equipmentManager;

    public Enemy CurrentEnemy => _enemy;
    public Boss CurrentBoss => _boss;

    int _stageCount;
    int _killCount; 
    int _enemyCount;
    float _gold;
    int _level;
    float _sum;
    float _cost;
    float _upgradeAmount;
    float _rebrithPoint;

    bool _bossIn = false;
    bool _isChangingStage = false;

    public float Gold => _gold;
    public int StageCount => _stageCount;

    public void Play()
    {
        _stageCount = 1;
        _view.UpdateStageText(_stageCount);
        _backGroundCtlr.ChangeBackGround(_stageCount);

        _killCount = 0;
        _enemyCount = 3;
        _view.UpdateKillText(_killCount, _enemyCount);

        _gold = 0f;
        _view.UpdateGoldText(0f, _gold);

        _rebrithPoint = 0f;
        _view.UpdateRebrithPointText(0f, _rebrithPoint);

        SpawnEnemy();

        _upgraderView.UpdateView(_level, _sum, _cost, _upgradeAmount);

        _view.HideBossTime();
    }

    public void TestRebirth()
    {
        _rebirth.RebirthEffect(() =>
        {
            Debug.Log("리버스 효과 완료");
        });
    }
    public void Rebirth()
    {
        _isBossBattleActive = false;
        StopBossTimer();

        float rewardRebirthPoint = _rebirthData.GetRebirthPointByStage(_stageCount);

        AddRebrithPoint(rewardRebirthPoint);

        _upgrader.ResetUpgrade();

        _allyCtrl.AllyAllDestroy();

        _allyCtrl.ResetAlly();

        if (_enemy != null)
        {
            _enemy.DestroyEnemy();
        }
        else
        {
            _boss.DestroyBoss();
            _bossIn = false;
        }

        _stageCount = 1;
        _view.UpdateStageText(_stageCount);
        _backGroundCtlr.ChangeBackGround(_stageCount);

        _killCount = 0;
        _enemyCount = 3;
        _view.UpdateKillText(_killCount, _enemyCount);

        _gold = 0f;
        _view.UpdateGoldText(0f, _gold);

        SpawnEnemy();
    }

    public void EnemyDead(float rewardGold, Vector3 deadPosition)
    {

        AddKillCount();

        AddGold(rewardGold * GetGoldWithEquipmentBonus());

        _view.UpdateKillText(_killCount, _enemyCount);

        _goldSpawner.GoldSpawnerView(deadPosition);

        _enemy = null;

        if (_bossIn == true)
        {
            SpawnBoss();
        }
        else
        {
            SpawnEnemy();
        }
    }

    public void BossDead(float rewardGold)
    {
        if (_isBossBattleActive == false)
            return;

        _isBossBattleActive = false;
        StopBossTimer();

        StartCoroutine(BossDeadRoutine(rewardGold));
    }

    private IEnumerator BossDeadRoutine(float rewardGold)
    {
        _isChangingStage = true;

        AddGold(rewardGold * GetGoldWithEquipmentBonus());

        float randomValue = Random.Range(0f, 100f);

        if (randomValue <= _equipmentRewardChance)
        {
            List<EquipmentData> selectedEquipments = _equipmentDropTable.GetRandomEquipmentsByStage(3, _stageCount);

            _equipmentRewardView.Show(selectedEquipments);

            yield return new WaitUntil(
                () => _equipmentRewardView.IsSelecting == false
            );

            EquipmentData selectedEquipment =
                _equipmentRewardView.SelectedEquipment;

            _equipmentManager.Equip(selectedEquipment);
        }
        else
        {
            Debug.Log("이번 보스는 장비 보상이 나오지 않았습니다.");
        }

        int nextStage = _stageCount + 1;
        bool showTransition = _backGroundCtlr.HasNextTransition(nextStage);

        if (showTransition)
        {
            string nextBackGroundName = _backGroundCtlr.GetBackGroundName(nextStage);
            TransitionImageData nextImageData = _backGroundCtlr.GetTransitionImageData(nextStage);

            yield return StartCoroutine(
                _stageTransitionView.Show(nextBackGroundName, nextImageData)
            );
        }

        _boss = null;
        _stageCount++;
        _killCount = 0;
        _enemyCount = 3;

        _view.UpdateStageText(_stageCount);
        _backGroundCtlr.ChangeBackGround(_stageCount);
        _view.UpdateKillText(_killCount, _enemyCount);

        _bossIn = false;

        SpawnEnemy();

        if (showTransition)
        {
            yield return StartCoroutine(_stageTransitionView.Hide());
        }

        _isChangingStage = false;
    }

    public void SpawnEnemy()
    {
        int randomIndex = Random.Range(0, _enemyPrefabs.Length);
        Enemy enemyprefab = _enemyPrefabs[randomIndex];

        _enemy = Instantiate(enemyprefab, _enemyParent);

        float maxHp = _data.GetHpByStage(_stageCount);
        float rewardGold = _data.GetGoldByStage(_stageCount);
        _enemy.Initialize(_enemyView, this, maxHp, rewardGold, _damageSpawner);
    }

    public void SpawnBoss()
    {
        _view.UpdateBossStageText(_stageCount);

        _enemyCount = 1;
        _view.UpdateKillText(_killCount, _enemyCount);

        int randomIndex = Random.Range(0, _bossPrefabs.Length);
        Boss bossprefab = _bossPrefabs[randomIndex];

        _boss = Instantiate(bossprefab, _bossParent);

        float maxHp = _data.GetHpByStage(_stageCount) * 3;
        float rewardGold = _data.GetGoldByStage(_stageCount) * 3;
        _boss.Initialize(_enemyView, this, maxHp, rewardGold, _damageSpawner);

        StartBossTimer();
    }

    public void StartBossTimer()
    {
        StopBossTimer();

        _bossRemainTime = _bossLimitTime;
        _isBossBattleActive = true;

        _view.ShowBossTime();
        _view.UpdateBossTime(_bossRemainTime);
        _view.StartBossTimeView(_bossLimitTime);    

        _bossTimerCoroutine = StartCoroutine(BossTimerRoutine());
    }

    IEnumerator BossTimerRoutine()
    {
        while (_bossRemainTime > 0f)
        {
            _bossRemainTime -= Time.deltaTime;

            if (_bossRemainTime < 0f)
                _bossRemainTime = 0f;

            _view.UpdateBossTime(_bossRemainTime);

            yield return null;
        }

        BossTimeOver();
    }

    void StopBossTimer()
    {
        if (_bossTimerCoroutine != null)
        {
            StopCoroutine(_bossTimerCoroutine);
            _bossTimerCoroutine = null;
        }

        _view.HideBossTime();
        _view.StopBossTimeView();
    }

    void BossTimeOver()
    {
        if (_isBossBattleActive == false)
            return;

        _isBossBattleActive = false;
        StopBossTimer();

        if (_boss != null)
        {
            _boss.DestroyBoss();
            _boss = null;
        }

        _bossIn = false;

        _killCount = 0;
        _enemyCount = 3;

        _view.UpdateStageText(_stageCount);
        _view.UpdateKillText(_killCount, _enemyCount);

        SpawnEnemy();
    }

    public void TapAttack()
    {
        if (_isChangingStage)
            return;

        if (_enemy != null)
            _hero.Attack(_enemy);
        else if (_boss != null)
            _hero.BossAttack(_boss);
    }

    public void AddKillCount()
    {
        _killCount++;

        if(_killCount >= _enemyCount)
        {
            if (_stageCount % 5 == 0)
            {
                _killCount = 0;
                _bossIn = true;
            }
            else 
            {
                _stageCount++;
                _killCount = 0;
                _view.UpdateStageText(_stageCount);
                _backGroundCtlr.ChangeBackGround(_stageCount);
            }
        }
    }

    public void AddGold(float amount)
    {
        float prevGold = _gold;
        _gold += amount;
        _view.UpdateGoldText(prevGold, _gold);
    }
    float GetGoldWithEquipmentBonus()
    {
        float bonusPercent = _equipmentManager.GoldGainBonusPercent;
        return (1f + bonusPercent / 100f);
    }

    public bool TryPayGold(float amount)
    {
        if (_gold >= amount)
        {
            float prevGold = _gold;
            _gold -= amount;
            _view.UpdateGoldText(prevGold, _gold);
            return true;
        }
        return false;
    }

    public void AddRebrithPoint(float amount)
    {
        float prevRebirthPoint = _rebrithPoint;
        _rebrithPoint += amount;
        _view.UpdateRebrithPointText(prevRebirthPoint, _rebrithPoint);
    }

    public bool TryPayRebirthhPoint(float amount)
    {
        if (_rebrithPoint >= amount)
        {
            float prevRebirthPoint = _rebrithPoint;
            _rebrithPoint -= amount;
            _view.UpdateRebrithPointText(prevRebirthPoint, _rebrithPoint);
            return true;
        }
        return false;
    }

    public IEnumerator PlayWithIntroTransition()
    {
        _isChangingStage = true;

        int firstStage = 1;

        string firstBackGroundName = _backGroundCtlr.GetBackGroundName(firstStage);
        TransitionImageData firstImageData = _backGroundCtlr.GetTransitionImageData(firstStage);

        yield return StartCoroutine(
            _stageTransitionView.Show(firstBackGroundName, firstImageData)
        );

        Play();

        yield return StartCoroutine(_stageTransitionView.Hide());

        _isChangingStage = false;
    }
}

