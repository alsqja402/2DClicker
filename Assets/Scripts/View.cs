using TMPro;
using DG.Tweening;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;

public class View : MonoBehaviour
{
    [SerializeField] TMP_Text _killText;

    [SerializeField] RectTransform stageTextRect;
    [SerializeField] TextMeshProUGUI _stageText;
    public void UpdateStageText(int stageCount)
    {
        _stageText.text = $"스테이지 {stageCount + 1}";

        // 애니메이션 겹침 방지
        stageTextRect.DOKill();

        // 스케일이 계속 커질 수 있음 
        stageTextRect.localScale = Vector3.one;

        Sequence seq = DOTween.Sequence();

        seq.Append(stageTextRect.DOScale(1.25f, 0.15f).SetEase(Ease.OutBack));
        seq.Append(stageTextRect.DOScale(1f, 0.2f).SetEase(Ease.InOutQuad));
    }

    public void UpdateKillText(int killCount, int enemyCount)
    {
        _killText.text = $"{killCount}/{enemyCount}";
    }
}

// Dotween sequence
// Append = 다음 컷 추가
// Join = 같은 시간에 다른 효과 추가
// Interval = 공백 구간
