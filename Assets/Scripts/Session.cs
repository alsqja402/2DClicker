using UnityEngine;

public class Session : MonoBehaviour
{
    [Header("컴포넌트")]
    [SerializeField] Hero _hero;
    [SerializeField] Enemy _enemy;

    [Header("컴포넌트")]
    [SerializeField] Enemy[] _enemyPrefabs;
    [SerializeField] Transform _enemyParent;
    
    public void Play()
    {
        SpawnEnemy();
    }

    public void EnemyDead()
    {
        SpawnEnemy();
    }

    public void SpawnEnemy()
    {
        int randomIndex = Random.Range(0, _enemyPrefabs.Length);
        Enemy enemyprefab = _enemyPrefabs[randomIndex];

        _enemy = Instantiate(enemyprefab, _enemyParent);
    }

    public void TapAttack()
    {
        _hero.Attack(_enemy);
    }
}
