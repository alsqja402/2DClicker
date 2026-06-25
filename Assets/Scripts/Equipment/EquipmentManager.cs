using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    [SerializeField] EquipmentData _equippedSword;
    [SerializeField] EquipmentData _equippedRing;
    [SerializeField] EquipmentView _equipmentView;

    public EquipmentData EquippedSword => _equippedSword;
    public EquipmentData EquippedRing => _equippedRing;
    public float DamageBonusPercent => GetBonus(EquipmentStatType.Damage);
    public float GoldGainBonusPercent => GetBonus(EquipmentStatType.GoldGain);

    private void Start()
    {
        _equipmentView.UpdateView(_equippedSword, _equippedRing);
    }

    public void Equip(EquipmentData equipment)
    {
        if (equipment == null)
            return;

        if (equipment.Type == EquipmentType.Sword)
        {
            _equippedSword = equipment;

            Debug.Log($"검 장착: {equipment.EquipmentName}");
        }
        else if (equipment.Type == EquipmentType.Ring)
        {
            _equippedRing = equipment;

            Debug.Log($"반지 장착: {equipment.EquipmentName}");
        }

        _equipmentView.UpdateView(_equippedSword, _equippedRing);
    }

    float GetBonus(EquipmentStatType statType)
    {
        float bonus = 0f;

        if (_equippedSword != null && _equippedSword.StatType == statType)
        {
            bonus += _equippedSword.StatValue;
        }

        if (_equippedRing != null && _equippedRing.StatType == statType)
        {
            bonus += _equippedRing.StatValue;
        }

        return bonus;
    }
}