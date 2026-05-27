using TMPro;
using UnityEngine;

public class SkillUpgraderView : MonoBehaviour
{
    [SerializeField] TMP_Text _levelText;
    [SerializeField] TMP_Text _damageText;
    [SerializeField] TMP_Text _costText;

    public void UpdateView(int level, float damageMultiple, float cost)
    {
        _levelText.text = $"Lv: {level}";
        _damageText.text = $"Smite : x{damageMultiple:0.0}";
        _costText.text = cost.ToString("0");
    }
}