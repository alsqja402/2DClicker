using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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

    [Header("이펙트")]
    [SerializeField] DamageSpawner _damageSpawner;
    [SerializeField] GoldSpawner _goldSpawner;
    [SerializeField] Particle _particle;
    [SerializeField] Rebirth _rebirth;

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

    }

    public void TestRebirth()
    {
        _rebirth.RebirthEffect(() =>
        {
            Debug.Log("실제 환생 처리");
        });
    }
    public void Rebirth()
    {
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

    public void EnemyDead(float rewardGold)
    {

        AddKillCount();

        AddGold(rewardGold);

        _view.UpdateKillText(_killCount, _enemyCount);

        _goldSpawner.GoldSpawnerView(_enemy.transform.position);

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
        _boss = null;
        _stageCount++;
        _killCount = 0;
        _enemyCount = 3;
        AddGold(rewardGold);
        _view.UpdateStageText(_stageCount);
        _backGroundCtlr.ChangeBackGround(_stageCount);
        _view.UpdateKillText(_killCount, _enemyCount);
        _bossIn = false;
        SpawnEnemy();
    }

    public void SpawnEnemy()
    {
        int randomIndex = Random.Range(0, _enemyPrefabs.Length);
        Enemy enemyprefab = _enemyPrefabs[randomIndex];

        _enemy = Instantiate(enemyprefab, _enemyParent);

        // 이거때매 적 스폰이 됐음 
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
    }

    public void TapAttack()
    {
        if (_enemy != null)
            _hero.Attack(_enemy);
        else
            _hero.BossAttack(_boss);
    }

    // 킬 수 증가 및 스테이지 증가
    public void AddKillCount()
    {
        _killCount++;

        if(_killCount >= _enemyCount)
        {
            // 보스가 출현하는 조건을 만족하면 보스 스폰
            if (_stageCount % 3 == 0)
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

    // 돈 지불하는거
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
}
