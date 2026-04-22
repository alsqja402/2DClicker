using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/SessionData")]
public class SessionData : ScriptableObject
{
    [SerializeField] float _baseHp;
    [SerializeField] float _hpMultiplier;

    [SerializeField] float _baseGold;
    [SerializeField] float _goldMultiplier;

    public float GetHpByStage(int stage)
    {
        if (stage <= 0)
        {
            return _baseHp;
        }

        return _baseHp * Mathf.Pow(_hpMultiplier, stage);
    }

    public float GetGoldByStage(int stage)
    {
        if (stage <= 0)
        {
            return _baseGold;
        }

        return _baseGold * Mathf.Pow(_goldMultiplier, stage);
    }
}
