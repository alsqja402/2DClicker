using UnityEngine;

public class RebirthUpgrader : MonoBehaviour
{
    [SerializeField] Session _session;

    [SerializeField] UpgradeData[] _datas;
    [SerializeField] RebirthUpgraderView[] _Rebirthviews;

    [SerializeField] int[] _levels;

    public void RebirthUpgrade(int index)
    {
        if (index < 0 || index >= _datas.Length)
            return;
        int level = _levels[index];
        float cost = _datas[index].GetCost(level);
        if (_session.TryPayRebirthhPoint(cost) == true)
        {
            float increaseAmount = _datas[index].GetIncreaseAmount(_levels[index]);
            float currentValue = 0;
            switch (index)
            {
                case 0:
                    if (_session.StageCount >= 1)
                    {
                    _levels[index]++;
                    _session.Rebirth();
                        Debug.Log("환생 성공");
                    }
                    else
                    {
                        Debug.Log("환생을 할 수 있는 스테이지가 아닙니다.");
                    }
                    break;
                case 1:
                    break;
                case 2:
                    break;
            }
            float nextCost = _datas[index].GetCost(_levels[index]);
            float nextIncreaseAmount = _datas[index].GetIncreaseAmount(_levels[index] + 1);


            _Rebirthviews[index].RebirthUpdateView(_levels[index], currentValue);
        }
        else
        {
            Debug.Log("리버스포인트가 부족합니다.");
        }
    }
}
