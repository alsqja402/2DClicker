using UnityEngine;
using DG.Tweening;

public class Boss : MonoBehaviour
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
    public Transform HitPoint => _hitPoint;

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
    public void DeathParticle(Vector3 pos)
    {
        // 파티클 없애는 작업 해야함
        Instantiate(_deathParticle,
            pos,
            Quaternion.identity);
    }

    void Die()
    {
        _session.BossDead(_model.RewardGold);

        DeathParticle(_deathParticlePoint.position);

        _spriteRenderer.DOFade(0f, _duration).OnComplete(() =>
        {
            Destroy(gameObject);
        });
    }
}
