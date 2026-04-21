using DG.Tweening;
using TMPro;
using UnityEngine;

public class DamageView : MonoBehaviour
{
    [Header("컴포넌트")]
    [SerializeField] TMP_Text _damageText;
    [SerializeField] RectTransform _rectTransform;

    [SerializeField] float _duration;  

    public RectTransform RectTransform => _rectTransform;

    Sequence _seq;

    public void UpdateDamage(float damage)
    {
        _damageText.text = damage.ToClickerString("{0:N0}");

        // 초기 상태 세팅
        _rectTransform.anchoredPosition = Vector2.zero;
        _rectTransform.localScale = Vector3.one;

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
}
