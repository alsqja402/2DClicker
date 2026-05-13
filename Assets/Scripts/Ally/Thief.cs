using UnityEngine;

public class Thief : MonoBehaviour
{
    [SerializeField] Session _session;

    [SerializeField] Animator _animator;

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
        _session.CurrentEnemy.TakeHit(_damage);
    }

    public void UpgradeThief(float amount)
    {
        _damage += amount;
        Debug.Log("시프 업그레이드");
    }
}