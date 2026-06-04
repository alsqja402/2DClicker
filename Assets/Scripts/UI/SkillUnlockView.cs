using DG.Tweening;
using UnityEngine;

public class SkillUnlockView : MonoBehaviour
{
    [SerializeField] GameObject _smitePanel;
    [SerializeField] GameObject _blessingPanel;

    public void OpenSmitePanel()
    {
        OpenPanel(_smitePanel);
    }

    public void CloseSmitePanel()
    {
        ClosePanel(_smitePanel);
    }

    public void OpenBlessingPanel()
    {
        OpenPanel(_blessingPanel);
    }

    public void CloseBlessingPanel()
    {
        ClosePanel(_blessingPanel);
    }

    void OpenPanel(GameObject panel)
    {
        panel.transform.DOKill();

        panel.transform.localScale = Vector3.zero;
        panel.SetActive(true);

        panel.transform.DOScale(1f, 0.5f);
    }

    void ClosePanel(GameObject panel)
    {
        panel.transform.DOKill();

        panel.transform.DOScale(0f, 0.5f)
        .OnComplete(() =>
        {
            panel.SetActive(false);
        });
    }
}