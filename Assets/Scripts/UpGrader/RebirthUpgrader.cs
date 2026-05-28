using UnityEngine;

public class RebirthUpgrader : MonoBehaviour
{
    [SerializeField] Session _session;
    [SerializeField] Skill _skill;
    [SerializeField] SkillUpgraderView _smiteUpgraderView;
    [SerializeField] SkillUpgraderView _blessingUpgraderView;

    [SerializeField] UpgradeData[] _datas;
    [SerializeField] RebirthUpgraderView[] _rebirthViews;

    [SerializeField] int[] _levels;

    [SerializeField] bool _isSmiteUnlocked = false;
    [SerializeField] bool _isBlessingUnlocked = false;

    private void Start()
    {
        // UpdateView() 안에서 이미 SetUnlocked()이 호출되고 있음.
        //_skillUpgraderView.SetUnlocked(_isSmiteUnlocked);
        _skill.SetSkill1Unlocked(_isSmiteUnlocked);
        _skill.SetSkill2Unlocked(_isBlessingUnlocked);
        UpdateSmiteView();
        UpdateBlessingView();
    }

    public void RebirthUpgrade(int index)
    {
        if (index < 0 || index >= _datas.Length)
            return;

        switch (index)
        {
            case 0:
                Rebirth();
                break;

            case 1:
                if (_isSmiteUnlocked == false)
                {
                    UnlockSmite(index);
                }
                else
                {
                    UpgradeSmite(index);
                }
                break;

            case 2:
                if (_isBlessingUnlocked == false)
                {
                    UnlockBlessing(index);
                }
                else
                {
                    UpgradeBlessing(index);
                }
                break;
        }
    }

    void Rebirth()
    {
        int index = 0;

        if (_session.StageCount >= 1)
        {
            _levels[index]++;

            _session.Rebirth();

            float currentValue = _levels[index];

            _rebirthViews[index].RebirthUpdateView(_levels[index], currentValue);

            Debug.Log("환생 성공");
        }
        else
        {
            Debug.Log("환생을 할 수 있는 스테이지가 아닙니다.");
        }
    }

    void UnlockSmite(int index)
    {
        int level = _levels[index];
        float cost = _datas[index].GetCost(level);

        if (_session.TryPayRebirthhPoint(cost) == false)
        {
            Debug.Log("리버스포인트가 부족합니다.");
            return;
        }

        _isSmiteUnlocked = true;

        _levels[index]++;

        // UpdateView()가 메뉴 아이콘 전환까지 같이 처리함.
        //_skillUpgraderView.SetUnlocked(true);
        _skill.SetSkill1Unlocked(true);
        UpdateSmiteView();

        Debug.Log("Smite 해금 성공");
    }

    void UpgradeSmite(int index)
    {
        int level = _levels[index];
        float cost = _datas[index].GetCost(level);

        if (_session.TryPayRebirthhPoint(cost) == false)
        {
            Debug.Log("리버스포인트가 부족합니다.");
            return;
        }

        float increaseAmount = _datas[index].GetIncreaseAmount(_levels[index]);

        _levels[index]++;

        _skill.IncreaseSkill1DamageMultiple(increaseAmount);

        UpdateSmiteView();

        Debug.Log("Smite 강화 성공");
    }

    void UpdateSmiteView()
    {
        int index = 1;

        int level = _levels[index];
        float currentValue = _skill.Skill1DamageMultiple;
        float cost = _datas[index].GetCost(level);
        float nextIncreaseAmount = _datas[index].GetIncreaseAmount(level + 1);

        string nameText = $"Smite : x{currentValue:0.0}";
        string valueText = $"+{nextIncreaseAmount:0.0}";

        _smiteUpgraderView.UpdateView(_isSmiteUnlocked, level, nameText, cost, valueText);
    }

    void UnlockBlessing(int index)
    {
        int level = _levels[index];
        float cost = _datas[index].GetCost(level);

        if (_session.TryPayRebirthhPoint(cost) == false)
        {
            Debug.Log("리버스포인트가 부족합니다.");
            return;
        }

        _isBlessingUnlocked = true;

        _levels[index]++;

        _skill.SetSkill2Unlocked(true);
        UpdateBlessingView();

        Debug.Log("Blessing 해금 성공");
    }

    void UpgradeBlessing(int index)
    {
        int level = _levels[index];
        float cost = _datas[index].GetCost(level);

        if (_session.TryPayRebirthhPoint(cost) == false)
        {
            Debug.Log("리버스포인트가 부족합니다.");
            return;
        }

        float increaseAmount = _datas[index].GetIncreaseAmount(_levels[index]);

        _levels[index]++;

        _skill.IncreaseSkill2Duration(increaseAmount);

        UpdateBlessingView();

        Debug.Log("Blessing 강화 성공");
    }

    void UpdateBlessingView()
    {
        int index = 2;

        int level = _levels[index];
        float currentValue = _skill.Skill2Duration;
        float cost = _datas[index].GetCost(level);
        float nextIncreaseAmount = _datas[index].GetIncreaseAmount(level + 1);

        string nameText = $"Blessing : {currentValue:0.0}s";
        string valueText = $"+{nextIncreaseAmount:0.0}s";

        _blessingUpgraderView.UpdateView(_isBlessingUnlocked, level, nameText, cost, valueText);
    }
}
