using UnityEngine;

public class Archer : MonoBehaviour
{
    [SerializeField] Session _session;
    [SerializeField] Bullet _arrowPrefab;

    [SerializeField] Animator _animator;
    [SerializeField] Transform _arrowSpawnPoint;

    [SerializeField] ParticleSystem _arrowParticle;

    [SerializeField] float _level;
    [SerializeField] float _arrowSpeed;
    [SerializeField] float _arrowDamage;
    [SerializeField] float _attackSpan;
    float _attackTimer;

    public float ArrowSpeed => _arrowSpeed;
    public float ArrowDamage => _arrowDamage;   
    public float AttackSpan => _attackSpan;

    public void Initialize(Session session, float arrowSpeed, float arrowDamage, float attackSpan)
    {
        _session = session;
        _arrowSpeed = arrowSpeed;
        _arrowDamage = arrowDamage;
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

    public void SpawnArrow()
    {
        Bullet
        arrow = Instantiate(_arrowPrefab);
        arrow.transform.position = _arrowSpawnPoint.position;
        arrow.Initialize(_session, _arrowSpeed, _arrowDamage, _arrowParticle);
    }

    public void UpgradeArcher(float amount)
    {
        _level++;
        _arrowDamage += amount;
        Debug.Log("아처 업그레이드");
    }
}
