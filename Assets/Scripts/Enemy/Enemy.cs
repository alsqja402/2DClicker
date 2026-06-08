using UnityEngine;
using DG.Tweening;
using Mono.Cecil.Cil;

public class Enemy : MonoBehaviour
{
    [Header("컴포넌트")]
    [SerializeField] EnemyModel _model;
    [SerializeField] EnemyView _view;
    [SerializeField] Transform _damageViewPoint;
    [SerializeField] float _duration;

    [SerializeField] ParticleSystem _deathParticle;
    [SerializeField] Transform _deathParticlePoint;

    [SerializeField] SpriteRenderer _spriteRenderer;
    [SerializeField] Transform _hitPoint;          

    Session _session;

    DamageSpawner _damageSpawner;
    bool _isDead;

    public Transform HitPoint => _hitPoint;
    
    public void Initialize(EnemyView view, Session session, float maxHp, float rewardGold, DamageSpawner damageSpawner)
    {
        _view = view;   
        _session = session;
        _damageSpawner = damageSpawner; 
        _isDead = false;
        _model.Initialize(maxHp, rewardGold);

        _view.UpdateHp(_model.CurrentHp, _model.MaxHp);
    }


    public void TakeHit(float damage, bool isCritical = false)
    {
        if (_isDead)
            return;

        _model.TakeDamage(damage);

        _damageSpawner.SpawnDamageView(_damageViewPoint.position, damage, isCritical);

        _view.UpdateHp(_model.CurrentHp, _model.MaxHp);

        if (_model.IsAlive == false)
        {
            Die();
        }
    }
    public void DeathParticle(Vector3 pos)
    {
        Instantiate(_deathParticle,
            pos,
            Quaternion.identity);
    }

    void Die()
    {
        if (_isDead)
            return;

        _isDead = true;

        Vector3 deadPosition = transform.position;

        _session.EnemyDead(_model.RewardGold, deadPosition);

        DeathParticle(_deathParticlePoint.position);

        _spriteRenderer.DOFade(0f, _duration).SetAutoKill().OnComplete(() =>
        {
            Destroy(gameObject);
        });
        //Destroy(gameObject);
    }

    public void DestroyEnemy()
    {
        Destroy(gameObject);
        Destroy(_model);
    }
}
