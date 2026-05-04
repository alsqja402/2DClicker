using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class GoldUI : MonoBehaviour
{
    static readonly Ease[] ScatterEasings =
    {
        Ease.OutQuad, Ease.OutCubic, Ease.OutSine, Ease.OutBack, Ease.OutCirc, Ease.OutExpo,
    };

    static readonly Ease[] MoveEasings =
    {
        Ease.InCirc, Ease.InQuad, Ease.InCubic, Ease.InOutCubic, Ease.InOutSine, Ease.InOutQuart,
        Ease.InSine, Ease.OutCubic, Ease.InExpo,
    };

    [SerializeField] Image _goldImage;

    [SerializeField] RectTransform _target;

    [Header("골드 퍼지는 범위와 시간")]
    [SerializeField] float _scatterRadius = 140f;
    [SerializeField] float _scatterDuration = 0.4f;
    [SerializeField] float _moveDuration = 1.2f;

    RectTransform _endPos;

    public void SetEndPosition(RectTransform endPos)
    {
        _endPos = endPos;
    }

    public void GoldMove()
    {
        _target.DOKill();
        _goldImage.DOKill();

        Sequence seq = DOTween.Sequence();

        seq.Append(_target.DOAnchorPos(_endPos.anchoredPosition, 1.5f).SetEase(Ease.InCirc));
        seq.AppendCallback(() => _goldImage.gameObject.SetActive(false));
    }

    public void GoldScatterThenMove()
    {
        _target.DOKill();
        _goldImage.DOKill();

        Vector2 origin = _target.anchoredPosition;
        Vector2 scatter = origin + Random.insideUnitCircle * _scatterRadius;

        Ease scatterEase = ScatterEasings[Random.Range(0, ScatterEasings.Length)];
        Ease moveEase = MoveEasings[Random.Range(0, MoveEasings.Length)];

        Sequence seq = DOTween.Sequence();
        seq.Append(_target.DOAnchorPos(scatter, _scatterDuration).SetEase(scatterEase));
        seq.Append(_target.DOAnchorPos(_endPos.anchoredPosition, _moveDuration).SetEase(moveEase));
        seq.AppendCallback(() => _goldImage.gameObject.SetActive(false));
    }
}
