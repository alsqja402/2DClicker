using DG.Tweening;
using UnityEngine;

public class TitleSlime : MonoBehaviour
{
    [Header("도망 이동")]
    [SerializeField] float moveX = 2f;
    [SerializeField] float moveDuration = 2f;

    [Header("대기 시간")]
    [SerializeField] float idleTime = 0.5f;

    [Header("시작 대기 시간")]
    [SerializeField] float startDelay = 0f;

    [Header("컴포넌트")]
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Animator animator;

    Vector3 startPosition;
    Sequence sequence;

    void Start()
    {
        startPosition = transform.position;
        Invoke(nameof(PlayTitleMotion), startDelay);
    }

    void PlayTitleMotion()
    {
        Vector3 middlePosition = startPosition - new Vector3(moveX * 0.5f, 0f, 0f);
        Vector3 leftPosition = startPosition - new Vector3(moveX, 0f, 0f);

        sequence = DOTween.Sequence();

        sequence.AppendCallback(() =>
        {
            spriteRenderer.flipX = true;
            animator.SetBool("IsRun", true);
        });

        sequence.Append(transform.DOMove(middlePosition, moveDuration * 0.5f).SetEase(Ease.Linear));

        sequence.AppendCallback(() =>
        {
            animator.SetBool("IsRun", false);
        });

        sequence.AppendInterval(idleTime);

        sequence.AppendCallback(() =>
        {
            spriteRenderer.flipX = true;
            animator.SetBool("IsRun", true);
        });

        sequence.Append(transform.DOMove(leftPosition, moveDuration * 0.5f).SetEase(Ease.Linear));

        sequence.AppendCallback(() =>
        {
            animator.SetBool("IsRun", false);
        });

        sequence.AppendInterval(idleTime);

        sequence.AppendCallback(() =>
        {
            spriteRenderer.flipX = false;
            animator.SetBool("IsRun", true);
        });

        sequence.Append(transform.DOMove(startPosition, moveDuration).SetEase(Ease.Linear));

        sequence.AppendCallback(() =>
        {
            animator.SetBool("IsRun", false);
        });

        sequence.AppendInterval(idleTime);

        sequence.SetLoops(-1);
    }

    void OnDestroy()
    {
        sequence?.Kill();

        if (animator != null)
        {
            animator.SetBool("IsRun", false);
        }
    }
}