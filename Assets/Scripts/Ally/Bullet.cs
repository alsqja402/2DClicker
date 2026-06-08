using UnityEngine;

// 불렛으로 고치기 
public class Bullet : MonoBehaviour
{
    [SerializeField] Session _session;

    [SerializeField] float _speed;
    [SerializeField] float _damage;

    ParticleSystem _hitParticle;

    Enemy _targetEnemy;
    Boss _targetBoss;
    Transform _targetHitPoint;
    Vector3 _targetPos;
    Vector3 _dir;

    public void Initialize(Session session, float speed, float damage, ParticleSystem hitParticle)
    {
        _session = session;
        _speed = speed;
        _damage = damage;
        _hitParticle = hitParticle;

        _targetEnemy = _session.CurrentEnemy;
        _targetBoss = _session.CurrentBoss;

        if(_targetEnemy != null && _targetEnemy.HitPoint != null)
        {
            _targetHitPoint = _targetEnemy.HitPoint;
        }
        else if (_targetBoss != null && _targetBoss.HitPoint != null)
        {
            _targetHitPoint = _targetBoss.HitPoint;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        _targetPos = _targetHitPoint.position;
        _dir = (_targetPos - transform.position).normalized;

        transform.right = _dir;
    }

    private void Update()
    {
        float distance = Vector3.Distance(_targetPos, transform.position);
        // 파티클

        if (distance < 0.1f)
        {
            if (_targetEnemy != null)
            {
                Instantiate(_hitParticle, transform.position, Quaternion.identity);
                _targetEnemy.TakeHit(_damage);
            }
            else if (_targetBoss != null)
            {
                Instantiate(_hitParticle, transform.position, Quaternion.identity);
                _targetBoss.TakeHit(_damage);
            }

            Destroy(gameObject);
        }
        else
        {
            transform.Translate(_dir * _speed * Time.deltaTime, Space.World);
        }
    }
}
