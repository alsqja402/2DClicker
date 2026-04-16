using UnityEngine;

public class EnemyModel : MonoBehaviour
{
    [SerializeField] float _maxHp;
    [SerializeField] float _currentHp;

    public bool IsAlive => _currentHp > 0;

    public void Initialize(float maxHp)
    {
        _maxHp = maxHp;
        _currentHp = _maxHp;
    }

    public void TakeDamage(float damage)
    {
        _currentHp = Mathf.Min(_currentHp - damage, _maxHp);
    }
}
