using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Menu : MonoBehaviour
{
    [Header("패널 설정")]
    [SerializeField] Button _openButton;
    [SerializeField] Button _closeButton;

    [SerializeField] RectTransform _panel;  

    [SerializeField] Vector2 _openPos;      
    [SerializeField] Vector2 _closePos;     

    [SerializeField] float _duration;

    [Header("업그레이드 설정")]
    [SerializeField] Hero _hero;
    [SerializeField] Button _damageUpButton;

    [SerializeField] Session _session;

    private Tweener _tween;

    private void Start()
    {
        _openButton.onClick.AddListener(Open);
        _closeButton.onClick.AddListener(Close);

        _panel.anchoredPosition = _openPos;

        _openButton.gameObject.SetActive(false);
        _closeButton.gameObject.SetActive(true);

        //_damageUpButton.onClick.AddListener(TryDamageUp);
    }

    public void Open()
    {
        _tween?.Kill();

        _tween = _panel.DOAnchorPos(_openPos, _duration)
                       .SetEase(Ease.OutCubic);

        _openButton.gameObject.SetActive(false);
        _closeButton.gameObject.SetActive(true);
    }

    public void Close()
    {
        _tween?.Kill();

        _tween = _panel.DOAnchorPos(_closePos, _duration)
                       .SetEase(Ease.InCubic);

        _openButton.gameObject.SetActive(true);
        _closeButton.gameObject.SetActive(false);
    }

    //public void TryDamageUp()
    //{
    //    if (_session.TryPayGold(10.0f))
    //    {
    //        _hero.DamageUp();
    //    }
    //    else
    //    {
    //        Debug.Log("골드가 부족합니다.");
    //    }
    //}
}