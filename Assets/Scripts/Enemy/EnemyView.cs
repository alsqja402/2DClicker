using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyView : MonoBehaviour
{
    [Header("ÄÄÆ÷³ÍÆ®")]
    [SerializeField] Image _hpBar;
    [SerializeField] TMP_Text _hpText;
    [SerializeField] TMP_Text _nameText;

    public void UpdateName(string name)
    {
        _nameText.text = name;
    }

    public void UpdateHp(float currenthp, float maxHp)
    {
        _hpBar.fillAmount = currenthp / maxHp;
    }
}
