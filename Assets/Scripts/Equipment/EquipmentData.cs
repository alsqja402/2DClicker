using UnityEngine;

public enum EquipmentGrade
{
    Rare,
    Epic,
    Unique,
    Legendary
}

public enum EquipmentType
{
    Sword,
    Ring
}

public enum EquipmentStatType
{
    Damage,
    GoldGain
}

[CreateAssetMenu(menuName = "ScriptableObjects/EquipmentData")]
public class EquipmentData : ScriptableObject
{
    [SerializeField] string _equipmentName;
    [SerializeField] EquipmentGrade _grade;
    [SerializeField] EquipmentType _type;
    [SerializeField] EquipmentStatType _statType;
    [SerializeField] float _statValue;

    public string EquipmentName => _equipmentName;
    public EquipmentGrade Grade => _grade;
    public EquipmentType Type => _type;
    public EquipmentStatType StatType => _statType;
    public float StatValue => _statValue;
}
