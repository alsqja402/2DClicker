using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Rebirth : MonoBehaviour
{
    [SerializeField] Image _fadeImage;
    [SerializeField] ParticleSystem _rebirthParticle;
    [SerializeField] Transform _particlePoint;

    public void RebirthEffect(Action onComplete)
    {
        Sequence sequence = DOTween.Sequence();

        sequence.Append(
            _fadeImage.DOFade(1f, 0.5f)
        );

        sequence.AppendInterval(0.5f);

        sequence.Append(
            _fadeImage.DOFade(0f, 0.5f)
        );

        sequence.AppendCallback(() =>
        {
            ParticleSystem particle = Instantiate(_rebirthParticle, _particlePoint.position, Quaternion.identity);
        });

        sequence.OnComplete(() =>
        {
            onComplete?.Invoke();
        });
    }
}