using TMPro;
using DG.Tweening;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;

public class View : MonoBehaviour
{
    [SerializeField] TMP_Text _killText;

    [SerializeField] RectTransform stageTextRect;
    [SerializeField] TMP_Text _stageText;

    [SerializeField] TMP_Text _goldText;
    [SerializeField] TMP_Text _rebrithPointText;

    public void UpdateStageText(int stageCount)
    {
        _stageText.text = $"Stage {stageCount}";
        _stageText.color = Color.white;

        // 애니메이션 겹침 방지
        stageTextRect.DOKill();

        // 스케일이 계속 커질 수 있음 
        stageTextRect.localScale = Vector3.one;

        Sequence seq = DOTween.Sequence();

        seq.Append(stageTextRect.DOScale(1.25f, 0.15f).SetEase(Ease.OutBack));
        seq.Append(stageTextRect.DOScale(1f, 0.2f).SetEase(Ease.InOutQuad));
    }
    public void UpdateBossStageText(int stageCount)
    {
        _stageText.text = $"Boss";
        _stageText.color = Color.red;

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
        _killText.text = $"{killCount + 1}/{enemyCount}";
    }

    public void UpdateGoldText(float prevGold, float gold)
    {
        DOVirtual.Float(prevGold, gold, 0.8f, value => _goldText.text = value.ToClickerString("{0:N0}"));
    }

    public void UpdateRebrithPointText(float prevRebirthPoint, float RebrithPoint)
    {
        DOVirtual.Float(prevRebirthPoint, RebrithPoint, 0.8f, value => _rebrithPointText.text = value.ToClickerString("{0:N0}"));
    }
}

// Dotween sequence
// Append = 다음 컷 추가
// Join = 같은 시간에 다른 효과 추가
// Interval = 공백 구간
