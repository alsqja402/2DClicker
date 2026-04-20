using UnityEngine;

public class Session : MonoBehaviour
{
    [Header("컴포넌트")]
    [SerializeField] Hero _hero;
    [SerializeField] Enemy _enemy;

    [SerializeField] View _view;

    [Header("컴포넌트")]
    [SerializeField] Enemy[] _enemyPrefabs;
    [SerializeField] Transform _enemyParent;
    [SerializeField] EnemyView _enemyView;

    [Header("적")]
    [SerializeField] float _baseHp;
    [SerializeField] float _hpMultiplier;  

    int _stageCount;
    int _killCount; 
    int _enemyCount = 3;

    public void Play()
    {
        _stageCount = 0;
        _view.UpdateStageText(_stageCount);

        _killCount = 0;
        _view.UpdateKillText(_killCount, _enemyCount);

        SpawnEnemy();
    }

    public void EnemyDead()
    {
        AddKillCount();

        //_stageCount++;
        _view.UpdateStageText(_stageCount);

        //_killCount++;
        _view.UpdateKillText(_killCount, _enemyCount);
        SpawnEnemy();
    }

    public void SpawnEnemy()
    {
        int randomIndex = Random.Range(0, _enemyPrefabs.Length);
        Enemy enemyprefab = _enemyPrefabs[randomIndex];

        _enemy = Instantiate(enemyprefab, _enemyParent);

        // 이거때매 적 스폰이 됐음 
        float maxHp = GetHpByStage(_stageCount);
        _enemy.Initialize(_enemyView, this, maxHp);
    }

    public void TapAttack()
    {
        _hero.Attack(_enemy);
    }

    public float GetHpByStage(int stage)
    {
        if(stage <= 0)
        {
            return _baseHp;
        }

        return _baseHp * Mathf.Pow(_hpMultiplier, stage);
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
}
