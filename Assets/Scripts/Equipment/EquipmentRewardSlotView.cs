using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentRewardSlotView : MonoBehaviour
{
    [SerializeField] Button _button;
    [SerializeField] TMP_Text _nameText;
    [SerializeField] TMP_Text _statText;
    [SerializeField] Image _equipmentImage;

    EquipmentData _equipment;
    EquipmentRewardView _rewardView;

    [SerializeField] RectTransform _rectTransform;
    [SerializeField] CanvasGroup _canvasGroup;

    public void UpdateView(
        EquipmentData equipment,
        EquipmentRewardView rewardView)
    {
        _equipment = equipment;
        _rewardView = rewardView;

        _nameText.text = equipment.EquipmentName;
        _statText.text = GetStatText(equipment);
        _equipmentImage.sprite = equipment.Icon;

        _button.onClick.RemoveAllListeners();
        _button.onClick.AddListener(SelectEquipment);
    }

    void SelectEquipment()
    {
        _rewardView.SelectSlot(this, _equipment);
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

    public void PlayShowAnimation(int index)
    {
        _button.interactable = false;

        _rectTransform.DOKill();
        _canvasGroup.DOKill();

        _canvasGroup.alpha = 0f;
        _rectTransform.localScale = Vector3.zero;
        _rectTransform.anchoredPosition = new Vector2(
            _rectTransform.anchoredPosition.x,
            -80f
        );

        float delay = index * 0.2f;

        Sequence sequence = DOTween.Sequence();

        sequence.AppendInterval(delay);

        sequence.Append(_canvasGroup.DOFade(1f, 0.25f));

        sequence.Join(
            _rectTransform.DOScale(1f, 0.45f)
                .SetEase(Ease.OutBack)
        );

        sequence.Join(
            _rectTransform.DOAnchorPosY(0f, 0.45f)
                .SetEase(Ease.OutCubic)
        );

        sequence.OnComplete(() =>
        {
            _button.interactable = true;
        });
    }

    public void PlaySelectAnimation(Action onComplete)
    {
        _button.interactable = false;

        _rectTransform.DOKill();
        _canvasGroup.DOKill();

        Sequence sequence = DOTween.Sequence();

        sequence.Append(
            _rectTransform.DOScale(1.1f, 0.25f)
                .SetEase(Ease.OutBack)
        );

        sequence.Append(
            _rectTransform.DOAnchorPosY(180f, 0.4f)
                .SetEase(Ease.InBack)
        );

        sequence.Join(
            _canvasGroup.DOFade(0f, 0.35f)
        );

        sequence.OnComplete(() =>
        {
            onComplete();
        });
    }

    public void PlayHideAnimation()
    {
        _button.interactable = false;

        _rectTransform.DOKill();
        _canvasGroup.DOKill();

        Sequence sequence = DOTween.Sequence();

        sequence.Join(
            _canvasGroup.DOFade(0f, 0.3f)
        );

        sequence.Join(
            _rectTransform.DOScale(0.85f, 0.3f)
                .SetEase(Ease.InBack)
        );
    }
}