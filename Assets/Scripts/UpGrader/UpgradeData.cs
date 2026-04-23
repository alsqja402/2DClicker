using UnityEngine;

// 1. 레벨별 비용
// 2. 레벨별 증가량

[CreateAssetMenu(menuName = "ScriptableObjects/UpgradeData")]
public class UpgradeData : ScriptableObject
{
    // 기본 비용 (ex: 10골드)
    // 비용 증가율 (ex: 1.5배)
    [SerializeField] float _baseCost;
    [SerializeField] float _costMultiplier;

    // 기본 스탯 증가량 (ex: 5데미지)
    // 스탯 증가량 증가율 (ex: 1.2배)
    [SerializeField] float _baseIncreaseAmount;
    [SerializeField] float _increaseAmountMultiplier;

    public float GetCost(int level)
    {
        // 비용 계산 로직
        return _baseCost * Mathf.Pow(_costMultiplier, level);
    }
    public float GetIncreaseAmount(int level)
    {
        // 증가량 계산 로직
        return _baseIncreaseAmount * Mathf.Pow(_increaseAmountMultiplier, level);
    }
}
