using System.Collections.Generic;
using UnityEngine;

public class DamageSpawner : MonoBehaviour
{
    [SerializeField] DamageView _damageViewPrefab;

    [SerializeField] RectTransform _damageSpawnPoint;

    //[SerializeField] List<DamageView> _damageViewPool = new List<DamageView>();

    //public DamageView CreatDamageView()
    //{
    //    DamageView damageView = Instantiate(_damageView, _damageSpawnPoint);

    //    damageView.gameObject.SetActive(false);

    //    _damageViewPool.Add(damageView);

    //    return damageView;
    //}

    //public DamageView PopDamageView()
    //{
    //    foreach(var damageView in _damageViewPool)
    //    {
    //        if (damageView.gameObject.activeInHierarchy == false)
    //        {
    //            damageView.gameObject.SetActive(true);
    //            return damageView;
    //        }
    //    }

    //    DamageView newDamageView = CreatDamageView();
    //    newDamageView.gameObject.SetActive(true);
    //    return newDamageView;
    //}

    public void SpawnDamageView(float damage) 
    {
        DamageView damageView = Instantiate(_damageViewPrefab, _damageSpawnPoint);
        damageView.UpdateDamage(damage);
    }   

    public void SpawnDamageView(Vector3 pos, float damage, bool isCritical = false)
    {
        DamageView damageView = Instantiate(_damageViewPrefab, _damageSpawnPoint);
        Vector3 screenPos = Camera.main.WorldToScreenPoint(pos);    // 스크린 좌표
        damageView.transform.position = screenPos;
        damageView.UpdateDamage(damage, isCritical);
    }
}
