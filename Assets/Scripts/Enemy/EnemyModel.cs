using UnityEngine;

public class EnemyModel : MonoBehaviour
{
    [SerializeField] float _maxHp;
    [SerializeField] float _currentHp;
    [SerializeField] float _rewardGold;


    // Getter 프로퍼티 축약 버전
    // (Getter란 멤버 변수를 외부에서 읽어 갈 수 있게 해주는 함수)
    public float MaxHp => _maxHp;
    public float CurrentHp => _currentHp;
    public bool IsAlive => _currentHp > Util.Epsilon;
    public float RewardGold => _rewardGold;

    public void Initialize(float maxHp, float rewardGold)
    {
        _maxHp = maxHp;
        _currentHp = _maxHp;

        _rewardGold = rewardGold;
    }

    public void TakeDamage(float damage)
    {
        _currentHp = Mathf.Min(_currentHp - damage, _maxHp);
    }
}
