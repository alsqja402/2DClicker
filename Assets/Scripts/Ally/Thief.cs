using UnityEngine;

public class Thief : MonoBehaviour
{
    [SerializeField] Session _session;

    [SerializeField] Animator _animator;

    [SerializeField] ParticleSystem _attackParticle;

    [SerializeField] float _damage;
    [SerializeField] float _level;
    [SerializeField] float _attackSpan;
    float _attackTimer;

    public float Damage => _damage; 
    public float AttackSpan => _attackSpan;

    public void Initialize(Session session, float damage, float attackSpan)
    {
        _session = session;
        _damage = damage;
        _attackSpan = attackSpan;
    }

    private void Update()
    {
        _attackTimer += Time.deltaTime;
        if (_attackTimer >= _attackSpan)
        {
            Attack();
            _attackTimer = 0f;
        }
    }

    public void Attack()
    {
        _animator.SetTrigger("OnAttack");
    }

    public void ThiefAttack()
    {
        Enemy targetEnemy = _session.CurrentEnemy;
        Boss targetBoss = _session.CurrentBoss;
        Transform hitPoint = null;

        if(targetEnemy != null && targetEnemy.HitPoint != null)
        {
            hitPoint = targetEnemy.HitPoint;
            targetEnemy.TakeHit(_damage);
        }
        else if (targetBoss != null && targetBoss.HitPoint != null)
        {
            hitPoint = targetBoss.HitPoint;
            targetBoss.TakeHit(_damage);  
        }
        else
        {
            return;
        }

        Instantiate(_attackParticle, hitPoint.position, Quaternion.identity);
    }

    public void UpgradeThief(float amount)
    {
        _damage += amount;
        Debug.Log("시프 업그레이드");
    }
}
