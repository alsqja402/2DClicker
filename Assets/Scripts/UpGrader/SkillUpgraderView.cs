using TMPro;
using UnityEngine;

public class SkillUpgraderView : MonoBehaviour
{
    [SerializeField] TMP_Text _levelText;
    [SerializeField] TMP_Text _nameText;
    [SerializeField] TMP_Text _costText;
    [SerializeField] TMP_Text _valueText;

    [SerializeField] GameObject _skillIcon;
    [SerializeField] GameObject _lockIcon;

    public void UpdateView(bool isUnlocked, int level, string nameText, float cost, string valueText)
    {
        SetUnlocked(isUnlocked);

        if (isUnlocked == false)
        {
            _levelText.text = "Locked";
            _nameText.text = nameText;
            _costText.text = $"Unlock: {cost:0}";
            _valueText.text = "";
            return;
        }

        _levelText.text = $"Lv: {level}";
        _nameText.text = nameText;
        _costText.text = cost.ToString("0");
        _valueText.text = valueText;
    }

    public void SetUnlocked(bool isUnlocked)
    {
        _skillIcon.SetActive(isUnlocked);
        _lockIcon.SetActive(!isUnlocked);
    }
}
