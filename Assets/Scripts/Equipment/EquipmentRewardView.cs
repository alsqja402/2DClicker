using System.Collections.Generic;
using UnityEngine;

public class EquipmentRewardView : MonoBehaviour
{
    [Header("화면")]
    [SerializeField] GameObject _equipmentpanel;
    [SerializeField] Transform _slotParent;

    [Header("등급별 슬롯 프리팹")]
    [SerializeField] EquipmentRewardSlotView _rareSlotPrefab;
    [SerializeField] EquipmentRewardSlotView _epicSlotPrefab;
    [SerializeField] EquipmentRewardSlotView _uniqueSlotPrefab;
    [SerializeField] EquipmentRewardSlotView _legendarySlotPrefab;
    public bool IsSelecting { get; private set; }
    public EquipmentData SelectedEquipment { get; private set; }

    public void Show(List<EquipmentData> equipments)
    {
        RemoveAllSlots();

        SelectedEquipment = null;
        IsSelecting = true;

        _equipmentpanel.SetActive(true);

        for (int i = 0; i < equipments.Count; i++)
        {
            EquipmentData equipment = equipments[i];

            EquipmentRewardSlotView prefab =
                GetSlotPrefab(equipment.Grade);

            EquipmentRewardSlotView slot =
                Instantiate(prefab, _slotParent);

            slot.UpdateView(equipment, this);

            slot.PlayShowAnimation(i);
        }
    }

    public void SelectEquipment(EquipmentData equipment)
    {
        SelectedEquipment = equipment;
        IsSelecting = false;

        _equipmentpanel.SetActive(false);

        Debug.Log($"선택한 장비: {equipment.EquipmentName}");
    }

    public void SelectSlot(
    EquipmentRewardSlotView selectedSlot,
    EquipmentData equipment)
    {
        for (int i = 0; i < _slotParent.childCount; i++)
        {
            EquipmentRewardSlotView slot =
                _slotParent.GetChild(i).GetComponent<EquipmentRewardSlotView>();

            if (slot == selectedSlot)
            {
                continue;
            }

            slot.PlayHideAnimation();
        }

        selectedSlot.PlaySelectAnimation(() =>
        {
            SelectEquipment(equipment);
        });
    }

    void RemoveAllSlots()
    {
        for (int i = _slotParent.childCount - 1; i >= 0; i--)
        {
            Destroy(_slotParent.GetChild(i).gameObject);
        }
    }

    EquipmentRewardSlotView GetSlotPrefab(EquipmentGrade grade)
    {
        if (grade == EquipmentGrade.Rare)
            return _rareSlotPrefab;

        if (grade == EquipmentGrade.Epic)
            return _epicSlotPrefab;

        if (grade == EquipmentGrade.Unique)
            return _uniqueSlotPrefab;

        return _legendarySlotPrefab;
    }
}