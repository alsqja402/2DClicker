using UnityEngine;


public class Upgrader : MonoBehaviour
{
    [SerializeField] Session _session;
    [SerializeField] Hero _hero;

    [SerializeField] UpgradeData[] _datas;
    [SerializeField] UpgraderView[] _views;

    [SerializeField] int[] _levels;

    private void Start()
    {
        UpdateAllViews();
    }

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
            float increaseAmount = _datas[index].GetIncreaseAmount(_levels[index]);
            float currentValue = 0;
            // 증가량 적용 로직 필요
            switch (index)
            {
                case 0: 
                    _hero.IncreaseDamage(increaseAmount);
                    currentValue = _hero._playerDamage;
                    break;
                case 1: 
                    _hero.IncreaseCriMultiple(increaseAmount);
                    currentValue = _hero._criMultiple;
                    break;
                case 2:
                    _hero.IncreaseCriPercent(increaseAmount);
                    currentValue = _hero._criPercent;
                    break;
            }
            float nextCost = _datas[index].GetCost(_levels[index]);
            float nextIncreaseAmount = _datas[index].GetIncreaseAmount(_levels[index] + 1);
            

            _views[index].UpdateView(_levels[index], currentValue, nextCost, nextIncreaseAmount);
        }
        else
        {
            Debug.Log("골드가 부족합니다.");
        }
    }

    public void UpdateView(int index)
    {
        if (index < 0 || index >= _datas.Length)
            return;
        int level = _levels[index];
        float increaseAmount = _datas[index].GetIncreaseAmount(level + 1);
        float cost = _datas[index].GetCost(level);
        float currentValue = 0;
        switch (index)
        {
            case 0:
                currentValue = _hero._playerDamage;
                break;
            case 1:
                currentValue = _hero._criMultiple;
                break;
            case 2:
                currentValue = _hero._criPercent;
                break;
        }
        _views[index].UpdateView(level, currentValue, cost, increaseAmount);
    }

    /// <summary>
    /// UI 갱신
    /// </summary>
    public void UpdateAllViews()
    {
        for (int i = 0; i < _datas.Length; i++)
        {
            UpdateView(i);
        }
    }

    /// <summary>
    /// 환생했을 때 업그레이드 초기화
    /// </summary>
    public void ResetUpgrade()
    {
        // 업그레이드 레벨 초기화
        for (int i = 0; i < _levels.Length; i++)
        {
            _levels[i] = 0;
        }

        _hero.ResetStats();

        UpdateAllViews();
    }
}
