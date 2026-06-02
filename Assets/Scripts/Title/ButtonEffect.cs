using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [Header("마우스 올림 효과")]
    [SerializeField] private float hoverScale = 1.05f;
    [SerializeField] private float hoverDuration = 0.08f;

    [Header("클릭 효과")]
    [SerializeField] private float clickScale = 0.92f;
    [SerializeField] private float clickDuration = 0.08f;

    private Vector3 startScale;
    private bool isHovering;

    private void Awake()
    {
        startScale = transform.localScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovering = true;

        transform.DOKill();
        transform.DOScale(startScale * hoverScale, hoverDuration)
            .SetEase(Ease.OutBack);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovering = false;

        transform.DOKill();
        transform.DOScale(startScale, hoverDuration)
            .SetEase(Ease.OutQuad);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        isHovering = false;

        transform.DOKill();

        Sequence sequence = DOTween.Sequence();

        sequence.Append(transform.DOScale(startScale * clickScale, clickDuration)
            .SetEase(Ease.OutQuad));

        sequence.Append(transform.DOScale(startScale, clickDuration)
            .SetEase(Ease.OutBack));
    }

    private void OnDestroy()
    {
        transform.DOKill();
    }
}