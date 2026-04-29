using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlodUI : MonoBehaviour
{
    [SerializeField] Image _goldImage;

    [SerializeField] RectTransform EndPos;

    void GoldMove()
    {
        RectTransform StartPos = _goldImage.rectTransform;

        _goldImage.DOKill();

        Sequence seq = DOTween.Sequence();

        
    }

}
