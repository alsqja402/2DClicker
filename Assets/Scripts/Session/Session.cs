using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Session : MonoBehaviour
{
    [Header("컴포넌트")]
    [SerializeField] Hero _hero;
    [SerializeField] Enemy _enemy;

    [SerializeField] View _view;
    [SerializeField] GoldSpawner _goldSpawner;

    [Header("컴포넌트")]
    [SerializeField] Enemy[] _enemyPrefabs;
    [SerializeField] Transform _enemyParent;
    [SerializeField] EnemyView _enemyView;

    [Header("적")]
    [SerializeField] SessionData _data;

    [SerializeField] DamageSpawner _damageSpawner;

    [SerializeField] UpgraderView _upgraderView;

    [SerializeField] Particle _particle;

    //[SerializeField] SpriteRenderer _enemySprite;

    int _stageCount;
    int _killCount; 
    int _enemyCount = 3;
    float _gold;
    int _level;
    float _sum;
    float _cost;
    float _upgradeAmount;

    public float Gold => _gold;

    public void Play()
    {
        _stageCount = 0;
        _view.UpdateStageText(_stageCount);

        _killCount = 0;
        _view.UpdateKillText(_killCount, _enemyCount);

        _gold = 0f;
        _view.UpdateGoldText(0f, _gold);

        SpawnEnemy();

        _upgraderView.UpdateView(_level, _sum, _cost, _upgradeAmount);
    }

    public void EnemyDead(float rewardGold)
    {
        AddKillCount();

        //_particle.DeathParticle(_enemy.transform.position); 

        AddGold(rewardGold);

        _view.UpdateStageText(_stageCount);

        _view.UpdateKillText(_killCount, _enemyCount);

        _goldSpawner.GoldSpawnerView(_enemy.transform.position);

        SpawnEnemy();
    }

    public void SpawnEnemy()
    {
        int randomIndex = Random.Range(0, _enemyPrefabs.Length);
        Enemy enemyprefab = _enemyPrefabs[randomIndex];

        //_enemySprite.DOFade(1f, _duration).OnComplete(() =>
        //    {
        //        _enemy = Instantiate(enemyprefab, _enemyParent);
        //    });


        _enemy = Instantiate(enemyprefab, _enemyParent);

        // 이거때매 적 스폰이 됐음 
        float maxHp = _data.GetHpByStage(_stageCount);
        float rewardGold = _data.GetGoldByStage(_stageCount);
        _enemy.Initialize(_enemyView, this, maxHp, rewardGold, _damageSpawner);
    }

    public void TapAttack()
    {
        _hero.Attack(_enemy);
    }

    // 킬 수 증가 및 스테이지 증가
    public void AddKillCount()
    {
        _killCount++; 

        if(_killCount >= _enemyCount)
        {
            _stageCount++;
            _killCount = 0; 
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
}
