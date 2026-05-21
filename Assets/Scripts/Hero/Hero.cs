using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// 공격, 이동 모션
/// </summary>
public class Hero : MonoBehaviour
{
    [Header("컴포넌트")]
    [SerializeField] HeroModel _model;
    [SerializeField] HeroRenderer _renderer;

    [SerializeField] Session _session;

    [SerializeField] ParticleSystem _criAttackParticle;
    [SerializeField] ParticleSystem _attackParticle;

    [SerializeField] float _baseDamage = 10;
    [SerializeField] float _baseCriMultiple = 1.5f;
    [SerializeField] float _baseCriPercent = 0.05f;

    public float _playerDamage;
    public float _criMultiple;
    public float _criPercent;

    private void Start()
    {
        ResetStats();
    }

    public void Attack(Enemy enemy)
    {   
        float finalDamage = _playerDamage * _criMultiple;
        Vector3 hitpos;

        if (Random.value < _criPercent)
        {
            hitpos = _session.CurrentEnemy.HitPoint.position;
            Instantiate(_criAttackParticle, hitpos, Quaternion.identity);
            enemy.TakeHit(finalDamage, true);
            Debug.Log("크리");
        }
        else
        {
            hitpos = _session.CurrentEnemy.HitPoint.position;
            Instantiate(_attackParticle, hitpos, Quaternion.identity);
            enemy.TakeHit(_playerDamage);
        }

        _renderer.Attack();

        Debug.Log("Hero Attack!");
    }

    public void BossAttack(Boss boss)
    {
        float finalDamage = _playerDamage * _criMultiple;
        Vector3 hitpos;

        if (Random.value < _criPercent)
        {
            hitpos = _session.CurrentBoss.HitPoint.position;
            Instantiate(_criAttackParticle, hitpos, Quaternion.identity);
            boss.TakeHit(finalDamage, true);
            Debug.Log("크리");
        }
        else
        {
            hitpos = _session.CurrentBoss.HitPoint.position;
            Instantiate(_attackParticle, hitpos, Quaternion.identity);
            boss.TakeHit(_playerDamage);
        }

        _renderer.Attack();

        Debug.Log("Hero Attack!");
    }

    public void IncreaseDamage(float amount)
    {
        _playerDamage += amount;
    }
    public void IncreaseCriMultiple(float amount)
    {
        _criMultiple += amount;
    }
    public void IncreaseCriPercent(float amount)
    {
        _criPercent += amount;
    }

    public void ResetStats()
    {
        _playerDamage = _baseDamage;
        _criMultiple = _baseCriMultiple;
        _criPercent = _baseCriPercent;
    }
}
