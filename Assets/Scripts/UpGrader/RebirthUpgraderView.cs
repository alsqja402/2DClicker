using TMPro;
using UnityEngine;

public class RebirthUpgraderView : MonoBehaviour
{
    [SerializeField] TMP_Text _levelText;
    [SerializeField] TMP_Text _nameDamageText;

    [SerializeField] TMP_Text _CountText;
    [SerializeField] TMP_Text _stageText;

    [SerializeField] string _nameDamageFormat;


    public void UpdateNameDamageText(float sum)
    {
        _nameDamageText.text = string.Format(_nameDamageFormat, sum);
    }

    public void UpdateStageText()
    {
        _stageText.text = "Stage 5";
    }
    public void UpdateCount(int level)
    {
        _levelText.text = $"Count: {level}";
    }

    public void RebirthUpdateView(int level, float sum)
    {
        UpdateNameDamageText(sum);
        UpdateStageText();
        UpdateCount(level);
    }
}
