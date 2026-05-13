using UnityEngine;

// 불렛으로 고치기 
public class Bullet : MonoBehaviour
{
    [SerializeField] Session _session;

    [SerializeField] float _speed;
    [SerializeField] float _damage;

    Vector3 _targetPos;
    Vector3 _dir;

    public void Initialize(Session session, float speed, float damage)
    {
        _session = session;
        _speed = speed;
        _damage = damage;

        _targetPos = _session.CurrentEnemy.HitPoint.position;

        _dir = (_targetPos - transform.position).normalized;

        transform.right = _dir;
    }

    private void Update()
    {
        float distance = Vector3.Distance(_targetPos, transform.position);
        
        if(distance < 0.1f)
        {
            if (_session.CurrentEnemy != null)
            {
                _session.CurrentEnemy.TakeHit(_damage);
            }
            Destroy(gameObject);
        }
        else
        {
            transform.Translate(_dir * _speed * Time.deltaTime, Space.World);
        }
    }
}