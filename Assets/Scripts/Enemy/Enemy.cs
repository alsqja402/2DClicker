using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("컴포넌트")]
    [SerializeField] EnemyModel _model;
    [SerializeField] EnemyView _view;

    Session _session;

    public void Initialize(EnemyView view, Session session, float maxHp)
    {
        _view = view;   
        _session = session;
        _model.Initialize(maxHp);

        _view.UpdateHp(_model.CurrentHp, _model.MaxHp);
    }


    public void TakeHit(float damage)
    {
        _model.TakeDamage(damage);

        _view.UpdateHp(_model.CurrentHp, _model.MaxHp);

        if (_model.IsAlive == false)
        {
            Die();
        }
    }

    void Die()
    {
        _session.EnemyDead();

        Destroy(gameObject);
    }
}
