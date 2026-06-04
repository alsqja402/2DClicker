using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static BackGroundCtlr;

public class StageTransitionView : MonoBehaviour
{
    [SerializeField] CanvasGroup _canvasGroup;
    [SerializeField] TMP_Text _stageText;
    [SerializeField] Image _areaImage;
    [SerializeField] float _fadeSpeed = 2f;
    [SerializeField] float _waitTime = 0.5f;

    public IEnumerator Show(string areaName, TransitionImageData imageData)
    {
        gameObject.SetActive(true);

        _stageText.text = areaName;

        if (imageData != null)
        {
            _areaImage.sprite = imageData.image;
            _areaImage.rectTransform.anchoredPosition = imageData.position;
            _areaImage.rectTransform.sizeDelta = imageData.size;
            _areaImage.preserveAspect = true;
        }

        _canvasGroup.alpha = 0f;

        while (_canvasGroup.alpha < 1f)
        {
            _canvasGroup.alpha += Time.deltaTime * _fadeSpeed;
            yield return null;
        }

        yield return new WaitForSeconds(_waitTime);
    }

    public IEnumerator Hide()
    {
        while (_canvasGroup.alpha > 0f)
        {
            _canvasGroup.alpha -= Time.deltaTime * _fadeSpeed;
            yield return null;
        }

        gameObject.SetActive(false);
    }
}