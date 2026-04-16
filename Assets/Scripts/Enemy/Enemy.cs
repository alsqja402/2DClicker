using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("ùùùùùù?")]
    [SerializeField] EnemyModel _model;

    Session _session;

    public void Initialize(Session session, float maxHp)
    {
        _model.Initialize(maxHp);
    }


    public void TakeHit(float damage)
    {
        _model.TakeDamage(damage);

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
