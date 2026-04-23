using UnityEngine;

// 1. 업그레이드

public class Upgrader : MonoBehaviour
{
    [SerializeField] Session _session;
    [SerializeField] Hero _hero;

    [SerializeField] UpgradeData[] _datas;
    [SerializeField] UpgraderView[] _views;

    [SerializeField] int[] _levels;


    /// <summary>
    /// 업그레이드 함수
    /// </summary>
    /// <param name="index"></param>
    public void Upgrade(int index)
    {
        if (index < 0 || index >= _datas.Length)
            return;
        int level = _levels[index];
        float cost = _datas[index].GetCost(level);
        if(_session.TryPayGold(cost) == true)
        {
            _levels[index]++;
            float increaseAmount = _datas[index].GetIncreaseAmount(level);
            // 증가량 적용 로직 필요
            switch (index)
            {
                case 0: 
                    _hero.IncreaseDamage(increaseAmount);
                    break;
                case 1: 
                    _hero.IncreaseCriMultiple(increaseAmount);
                    break;
                case 2:
                    _hero.IncreaseCriPercent(increaseAmount);
                    break;
            }

            _views[index].UpdateView(_levels[index], increaseAmount);
        }
        else
        {
            Debug.Log("골드가 부족합니다.");
        }
    }
}
