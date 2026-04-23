using TMPro;
using UnityEngine;

public class UpgraderView : MonoBehaviour
{
    [SerializeField] TMP_Text _levelText;
    [SerializeField] TMP_Text _nameDamageText;

    [SerializeField] string _nameDamageFormat;


    public void UpdateLevel(int level)
    {
        _levelText.text = $"Lv: {level}";
    }

    public void UpdateNameDamageText(float sum)
    {
        _nameDamageText.text = sum.ToString(_nameDamageFormat);
    }

    public void UpdateView(int level, float sum)
    {
        UpdateLevel(level);
        UpdateNameDamageText(sum);
    }
}
