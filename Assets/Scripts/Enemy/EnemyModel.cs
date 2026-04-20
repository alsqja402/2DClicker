using UnityEngine;

public class EnemyModel : MonoBehaviour
{
    [SerializeField] float _maxHp;
    [SerializeField] float _currentHp;


    // Getter 프로퍼티 축약 버전
    // (Getter란 멤버 변수를 외부에서 읽어 갈 수 있게 해주는 함수)
    public float MaxHp => _maxHp;
    public float CurrentHp => _currentHp;
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
