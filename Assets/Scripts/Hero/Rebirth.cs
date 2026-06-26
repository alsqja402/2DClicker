using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Rebirth : MonoBehaviour
{
    [SerializeField] Image _fadeImage;
    [SerializeField] ParticleSystem _rebirthParticle;
    [SerializeField] Transform _particlePoint;

    public void RebirthEffect(Action onScreenCovered, Action onComplete = null)
    {
        Sequence sequence = DOTween.Sequence();

        sequence.Append(
            _fadeImage.DOFade(1f, 0.5f)
        );

        sequence.AppendInterval(0.5f);

        sequence.AppendCallback(() =>
        {
            onScreenCovered?.Invoke();
        });

        sequence.Append(
            _fadeImage.DOFade(0f, 0.5f)
        );

        sequence.AppendCallback(() =>
        {
            Instantiate(_rebirthParticle, _particlePoint.position, Quaternion.identity);
        });

        // 연출 끝난 뒤 실행할 코드가 있으면 사용할 것 (예: 버튼 막기 등)
        sequence.OnComplete(() =>
        {
            onComplete?.Invoke();
        });
    }
}