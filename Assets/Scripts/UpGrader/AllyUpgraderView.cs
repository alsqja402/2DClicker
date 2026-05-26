using TMPro;
using UnityEngine;

public class AllyUpgraderView : MonoBehaviour
{
    [SerializeField] TMP_Text _levelText;
    [SerializeField] TMP_Text _nameDamageText;

    [SerializeField] TMP_Text _upgradeButtonCostText;
    [SerializeField] TMP_Text _upgradeButtonValueText;

    [SerializeField] string _nameDamageFormat;


    public void UpdateLevel(int level)
    {
        _levelText.text = $"Lv: {level}";
    }

    public void UpdateNameDamageText(float sum)
    {
        _nameDamageText.text = string.Format(_nameDamageFormat, sum);
        _nameDamageText.text = sum.ToClickerString(_nameDamageFormat);
    }

    public void UpdateUpgradeButtonCostText(float cost)
    {
        _upgradeButtonCostText.text = cost.ToClickerString("{0:N0}");
    }

    public void UpdateUpgradeButtonValueText(float value)
    {
        _upgradeButtonValueText.text = value.ToClickerString("+{0:F2}");
    }

    public void UpdateView(int level, float sum, float cost, float value)
    {
        UpdateLevel(level);
        UpdateNameDamageText(sum);
        UpdateUpgradeButtonCostText(cost);
        UpdateUpgradeButtonValueText(value);
    }
}
