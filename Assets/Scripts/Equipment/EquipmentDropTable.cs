using System.Collections.Generic;
using UnityEngine;

public class EquipmentDropTable : MonoBehaviour
{
    [SerializeField] List<EquipmentData> _equipments;

    public List<EquipmentData> GetRandomEquipments(int count)
    {
        List<EquipmentData> result = new List<EquipmentData>();
        List<EquipmentData> candidates = new List<EquipmentData>(_equipments); // _equipment를 바로 사용하지 않고, 복사하여 사용(RemoveAt을 사용하기 위해)

        for (int i = 0; i < count; i++)
        {
            if (candidates.Count <= 0)
            {
                break;
            }

            int randomIndex = Random.Range(0, candidates.Count);
            EquipmentData selectedEquipment = candidates[randomIndex];

            result.Add(selectedEquipment);
            candidates.RemoveAt(randomIndex);
        }

        return result;
    }

    public List<EquipmentData> GetRandomEquipmentsByStage(int count, int stage)
    {
        // 이번에 뽑을 수 있는 후보 장비 목록
        List<EquipmentData> candidates = new List<EquipmentData>();

        for (int i = 0; i < _equipments.Count; i++)
        {
            EquipmentData equipment = _equipments[i];

            // 이 장비가 지금 스테이지에서 나와도 되는가?
            if (CanDropByStage(equipment.Grade, stage))
            {
                candidates.Add(equipment);
            }
        }
        
        // 뽑힌 후보 장비들
        List<EquipmentData> result = new List<EquipmentData>();

        // 후보 장비들 중에서 랜덤으로 count개 뽑기
        for (int i = 0; i < count; i++)
        {
            // 후보 장비가 없으면 뽑기 멈춤 (갯수가 부족할 때)
            if (candidates.Count <= 0)
            {
                break;
            }

            // 후보 장비들 중에서 랜덤 번호 뽑기
            int randomIndex = Random.Range(0, candidates.Count);
            EquipmentData selectedEquipment = candidates[randomIndex];

            // 뽑힌 후보 장비는 선택 장비에 추가하고, 후보 목록에서는 제거하기 (중복 방지)
            result.Add(selectedEquipment);
            candidates.RemoveAt(randomIndex);
        }

        return result;
    }

    // 등급 제한
    bool CanDropByStage(EquipmentGrade grade, int stage)
    {
        if (stage < 10)
        {
            return grade == EquipmentGrade.Rare ||
                   grade == EquipmentGrade.Epic;
        }

        if (stage < 20)
        {
            return grade == EquipmentGrade.Rare ||
                   grade == EquipmentGrade.Epic ||
                   grade == EquipmentGrade.Unique;
        }

        return grade == EquipmentGrade.Epic ||
               grade == EquipmentGrade.Unique ||
               grade == EquipmentGrade.Legendary;
    }

    // 목표: 인벤토리창을 만들고 뽑힌 장비는 인벤토리에 저장. 장비를 장착할 수 있고, 장착하면 플레이어의 능력치가 올라가는 기능 만들기
    // 높은 등급의 장비를 장착하고 남은 낮은 장비들은, 판매하거나 같은 등급의 장비와 합성해서 더 높은 등급의 장비로 만들 수 있게 만들기
}