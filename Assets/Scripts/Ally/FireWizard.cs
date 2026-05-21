using UnityEngine;

public class FireWizard : MonoBehaviour
{
    [SerializeField] Session _session;
    [SerializeField] Bullet _fireBallPrefab;

    [SerializeField] Animator _animator;
    [SerializeField] Transform _fireBallSpawnPoint;

    [SerializeField] ParticleSystem _fireBallParticle;

    [SerializeField] float _level;
    [SerializeField] float _fireBallSpeed;
    [SerializeField] float _fireBallDamage;
    [SerializeField] float _attackSpan;
    float _attackTimer;

    public float FireBallSpeed => _fireBallSpeed;
    public float FireBallDamage => _fireBallDamage;
    public float AttackSpan => _attackSpan;

    public void Initialize(Session session, float fireBallSpeed, float fireBallDamage, float attackSpan)
    {
        _session = session;
        _fireBallSpeed = fireBallSpeed;
        _fireBallDamage = fireBallDamage;
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
        Debug.Log("어택 실행");
        _animator.SetTrigger("OnAttack");
    }

    public void SpawnFireBall()
    {
        Debug.Log("파이어볼 생성");
        Bullet fireBall = Instantiate(_fireBallPrefab);
        fireBall.transform.position = _fireBallSpawnPoint.position;
        fireBall.Initialize(_session, _fireBallSpeed, _fireBallDamage, _fireBallParticle);
    }

    public void UpgradeFireWizard(float amount)
    {
        _fireBallDamage += amount;
        Debug.Log("파위 업그레이드");
    }
}