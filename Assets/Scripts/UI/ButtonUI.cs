using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIButtonHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] RectTransform _target;

    [SerializeField] float _scaleValue;

    // 마우스 올렸다가 뗐을 때 시간
    [SerializeField] float _duration;
    // 클릭했을 때 시간
    [SerializeField] float _clickDuration;

    Vector3 _scale;

    private void Awake()
    {
        _scale = _target.localScale;

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _target.DOScale(_scaleValue, _duration).SetEase(Ease.OutBack);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _target.DOScale(1f, _duration).SetEase(Ease.InBack);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Sequence seq = DOTween.Sequence();

        _target.DOKill();
        
        seq.Append(_target.DOScale(_scale * 0.9f, _clickDuration).SetEase(Ease.OutBack));
        seq.Append(_target.DOScale(1f, _clickDuration).SetEase(Ease.InBack));
    }
}