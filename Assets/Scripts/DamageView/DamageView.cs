using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DamageView : MonoBehaviour
{
    [Header("컴포넌트")]
    [SerializeField] TMP_Text _damageText;
    [SerializeField] RectTransform _rectTransform;

    [SerializeField] float _duration;

    [Header("데미지 뷰")]
    [SerializeField] Color _normalColor;
    [SerializeField] Color _criticalColor;
    [SerializeField] Image _damageImage;
    [SerializeField] RectTransform _damageImageRect;

    public RectTransform RectTransform => _rectTransform;

    Sequence _seq;


    public void UpdateDamage(float damage, bool isCritical = false)
    {
        if (isCritical == false)
        {
            _damageImage.enabled = false;
            _damageText.text = damage.ToClickerString("{0:N0}");
            UpdateFirePosition();

            // 초기 상태 세팅
            _rectTransform.anchoredPosition = Vector2.zero;
            _rectTransform.localScale = Vector3.one;

            _damageText.color = _normalColor;
            Color color = _damageText.color;
            color.a = 1f;
            _damageText.color = color;

            // 기존 트윈 제거 (중요)
            _seq?.Kill();

            _seq = DOTween.Sequence();

            // 랜덤으로 올라가기 
            float randomX = Random.Range(-30f, 30f);
            _seq.Join(
                _rectTransform.DOAnchorPos(new Vector2(randomX, 100f), _duration).SetEase(Ease.OutCubic)
            );

            // 사라지기
            _seq.Join(
                _damageText.DOFade(0f, _duration)
            );

            // 팝 효과 (선택)
            _seq.Join(
                _rectTransform.DOScale(1.2f, 0.2f).From(0.5f)
            );

            // 5. 종료 처리
            _seq.OnComplete(() =>
            {
                gameObject.SetActive(false);
            });
        }
        else
        {
            _damageImage.enabled = true;
            _damageText.text = damage.ToClickerString("{0:N0}");
            UpdateFirePosition();

            // 초기 상태 세팅
            _rectTransform.anchoredPosition = Vector2.zero;
            _rectTransform.localScale = Vector3.one;

            _damageText.color = _criticalColor;
            Color color = _damageText.color;
            color.a = 1f;
            _damageText.color = color;
            Color imageColor = _damageImage.color;
            imageColor.a = 1f;
            _damageImage.color = imageColor;

            // 기존 트윈 제거 (중요)
            _seq?.Kill();

            _seq = DOTween.Sequence();

            // 랜덤으로 올라가기 
            float randomX = Random.Range(-40f, 40f);
            _seq.Join(
                _rectTransform.DOAnchorPos(new Vector2(randomX, 100f), _duration).SetEase(Ease.OutCubic)
            );

            // 사라지기
            _seq.Join(
                _damageText.DOFade(0f, _duration)
            );

            _seq.Join(
                _damageImage.DOFade(0f, _duration)
            );

            // 팝 효과 (선택)
            _seq.Join(
                _rectTransform.DOScale(1.5f, 0.3f).From(0.5f)
            );

            // 5. 종료 처리
            _seq.OnComplete(() =>
            {
                gameObject.SetActive(false);
            });
        }
    }

    void UpdateFirePosition()
    {
        float width = _damageText.preferredWidth;
        _damageImageRect.anchoredPosition = new Vector2(width - 30f, 30f);
    }
}
