using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyView : MonoBehaviour
{
    [Header("컴포넌트")]
    [SerializeField] Image _forntHpBar;
    [SerializeField] Image _backHpBar;
    [SerializeField] TMP_Text _hpText;
    [SerializeField] TMP_Text _nameText;

    public void UpdateName(string name)
    {
        _nameText.text = name;
    }

    public void UpdateHp(float currentHp, float maxHp)
    {
        float targetValue = currentHp / maxHp;

        _backHpBar.DOFillAmount(targetValue, 0.3f).SetEase(Ease.OutCubic);
        _forntHpBar.fillAmount = (currentHp / maxHp);
        _hpText.text = currentHp.ToClickerString("{0:N0} HP");
    }
}
