using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class AdviceView : MonoBehaviour
{
    [SerializeField] Button _adviceButton;
    [SerializeField] Button _closeButton;

    [SerializeField] GameObject _advicePanel;

    private Tweener _tweener;

    private void Start()
    {
        _adviceButton.onClick.AddListener(AdviceOpen);
        _closeButton.onClick.AddListener(AdviceClose);

        _advicePanel.SetActive(false);
    }

    void AdviceOpen()
    {
        _advicePanel.transform.localScale = Vector3.zero;

        _advicePanel.SetActive(true);
        _advicePanel.transform.DOScale(1f, 0.3f);
    }

    void AdviceClose()
    {
        _advicePanel.transform.DOScale(0f, 0.3f)
        .OnComplete(() =>
        {
            _advicePanel.SetActive(false);
        });
    }
}
