using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("컴포넌트")]
    [SerializeField] EnemyModel _model;
    [SerializeField] EnemyView _view;
    [SerializeField] Transform _damageViewPoint;

    Session _session;

    DamageSpawner _damageSpawner;

    public void Initialize(EnemyView view, Session session, float maxHp, float rewardGold, DamageSpawner damageSpawner)
    {
        _view = view;   
        _session = session;
        _damageSpawner = damageSpawner; 
        _model.Initialize(maxHp, rewardGold);

        _view.UpdateHp(_model.CurrentHp, _model.MaxHp);
    }


    public void TakeHit(float damage, bool isCritical = false)
    {
        _model.TakeDamage(damage);

        _damageSpawner.SpawnDamageView(_damageViewPoint.position, damage, isCritical);

        _view.UpdateHp(_model.CurrentHp, _model.MaxHp);

        if (_model.IsAlive == false)
        {
            Die();
        }
    }

    void Die()
    {
        _session.EnemyDead(_model.RewardGold);

        Destroy(gameObject);
    }
}
