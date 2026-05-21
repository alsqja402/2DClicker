using UnityEngine;

public class DarkWizard : MonoBehaviour
{
    [SerializeField] Session _session;
    [SerializeField] Bullet _darkBallPrefab;

    [SerializeField] Animator _animator;
    [SerializeField] Transform _darkBallSpawnPoint;

    [SerializeField] ParticleSystem _darkBallParticle;

    [SerializeField] float _level;
    [SerializeField] float _darkBallSpeed;
    [SerializeField] float _darkBallDamage;
    [SerializeField] float _attackSpan;
    float _attackTimer;

    public float DarkBallSpeed => _darkBallSpeed;
    public float DarkBallDamage => _darkBallDamage;
    public float AttackSpan => _attackSpan;

    public void Initialize(Session session, float darkBallSpeed, float darkBallDamage, float attackSpan)
    {
        _session = session;
        _darkBallSpeed = darkBallSpeed;
        _darkBallDamage = darkBallDamage;
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

    public void SpawnDarkBall()
    {
        Bullet darkBall = Instantiate(_darkBallPrefab);
        darkBall.transform.position = _darkBallSpawnPoint.position;
        darkBall.Initialize(_session, _darkBallSpeed, _darkBallDamage, _darkBallParticle);
    }

    public void UpgradeDarkWizard(float amount)
    {
        _darkBallDamage += amount;
        Debug.Log("다위 업그레이드");
    }
}