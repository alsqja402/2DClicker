using TMPro;
using UnityEngine;

public class EquipmentRewardSlotView : MonoBehaviour
{
    [SerializeField] TMP_Text _nameText;
    [SerializeField] TMP_Text _statText;

    EquipmentData _equipment;

    public void UpdateView(EquipmentData equipment)
    {
        _equipment = equipment;

        _nameText.text = equipment.EquipmentName;
        _statText.text = GetStatText(equipment);
    }

    string GetStatText(EquipmentData equipment)
    {
        if (equipment.StatType == EquipmentStatType.Damage)
        {
            return $"Damage +{equipment.StatValue}%";
        }

        if (equipment.StatType == EquipmentStatType.GoldGain)
        {
            return $"Gold +{equipment.StatValue}%";
        }

        return "";
    }
}