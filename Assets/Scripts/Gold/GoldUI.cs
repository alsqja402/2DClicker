using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class GoldUI : MonoBehaviour
{
    [SerializeField] Image _goldImage;

    [SerializeField] RectTransform _target;

    RectTransform _endPos;

    public void SetEndPosition(RectTransform endPos)
    {
        _endPos = endPos;
    }

    public void GoldMove()
    {
        //RectTransform StartPos = _goldImage.rectTransform;

        //Vector3 screenPos = Camera.main.WorldToScreenPoint(pos);
        //Debug.Log("screenPos: " + screenPos);
        //StartPos.position = screenPos;

        _goldImage.DOKill();

        Sequence seq = DOTween.Sequence();

        seq.Append(_target.DOAnchorPos(_endPos.anchoredPosition, 1.5f).SetEase(Ease.InCirc));
        seq.AppendCallback(() => _goldImage.gameObject.SetActive(false));
    }

}
