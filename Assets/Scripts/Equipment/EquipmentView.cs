using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentView : MonoBehaviour
{
    [SerializeField] GameObject _panel;
    [SerializeField] RectTransform _panelRectTransform;
    [SerializeField] CanvasGroup _panelCanvasGroup;

    [Header("검")]
    [SerializeField] Image _swordImage;
    [SerializeField] TMP_Text _swordNameText;
    [SerializeField] TMP_Text _swordStatText;

    [Header("반지")]
    [SerializeField] Image _ringImage;
    [SerializeField] TMP_Text _ringNameText;
    [SerializeField] TMP_Text _ringStatText;

    public void Open()
    {
        _panel.SetActive(true);

        _panelRectTransform.DOKill();
        _panelCanvasGroup.DOKill();

        _panelCanvasGroup.alpha = 0f;
        _panelRectTransform.localScale = Vector3.one * 0.85f;

        _panelCanvasGroup.DOFade(1f, 0.25f);

        _panelRectTransform
            .DOScale(1f, 0.25f)
            .SetEase(Ease.OutBack);
    }

    public void Close()
    {
        _panelRectTransform.DOKill();
        _panelCanvasGroup.DOKill();

        Sequence sequence = DOTween.Sequence();

        sequence.Append(
            _panelRectTransform
                .DOScale(0.85f, 0.18f)
                .SetEase(Ease.InBack)
        );

        sequence.Join(
            _panelCanvasGroup.DOFade(0f, 0.18f)
        );

        sequence.OnComplete(() =>
        {
            _panel.SetActive(false);
            _panelRectTransform.localScale = Vector3.one;
        });
    }

    public void UpdateView(EquipmentData sword, EquipmentData ring)
    {
        UpdateSwordView(sword);
        UpdateRingView(ring);
    }

    void UpdateSwordView(EquipmentData sword)
    {
        if (sword == null)
        {
            _swordImage.gameObject.SetActive(false);
            _swordNameText.text = "not equipped";
            _swordStatText.text = "";
            return;
        }

        _swordImage.gameObject.SetActive(true);
        _swordImage.sprite = sword.Icon;
        _swordNameText.text = sword.EquipmentName;
        _swordStatText.text = GetStatText(sword);
    }

    void UpdateRingView(EquipmentData ring)
    {
        if (ring == null)
        {
            _ringImage.gameObject.SetActive(false);
            _ringNameText.text = "not equipped";
            _ringStatText.text = "";
            return;
        }

        _ringImage.gameObject.SetActive(true);
        _ringImage.sprite = ring.Icon;
        _ringNameText.text = ring.EquipmentName;
        _ringStatText.text = GetStatText(ring);
    }

    string GetStatText(EquipmentData equipment)
    {
        if (equipment.StatType == EquipmentStatType.Damage)
        {
            return $"Damage +{equipment.StatValue}%";
        }

        if (equipment.StatType == EquipmentStatType.GoldGain)
        {
            return $"Gold +{equipment.StatValue}%";
        }

        return "";
    }
}