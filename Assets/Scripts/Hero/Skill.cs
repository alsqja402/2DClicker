using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Skill : MonoBehaviour
{
    [SerializeField] Hero _hero;
    [SerializeField] Session _session;

    [Header("스킬 1")]
    [SerializeField] float _skill1DamageMultiple = 5f;

    [SerializeField] ParticleSystem _skill1Particle;
    [SerializeField] Transform _skill1ParticlePoint;

    [SerializeField] float _skill1CoolTime;    
    bool _canUseSkill1 = true;

    [SerializeField] Button _skill1Button;
    [SerializeField] Image _skill1CoolTimeImage;
    [SerializeField] TMP_Text _skill1CoolTimeText;

    [Header("스킬 2")]
    [SerializeField] float _skill2DamageMultiple;
    [SerializeField] float _skill2Duration;
    [SerializeField] float _skill2CoolTime;

    bool _canUseSkill2 = true;
    bool _isSkill2Active = false;

    [SerializeField] Button _skill2Button;
    [SerializeField] Image _skill2CoolTimeImage;
    [SerializeField] TMP_Text _skill2CoolTimeText;
    [SerializeField] GameObject _skill2DurationUI;
    [SerializeField] Image _skill2DurationBar;

    public float Skill1DamageMultiple => _skill1DamageMultiple;
    public float Skill2Duration => _skill2Duration;
    public float Skill2CoolTime => _skill2CoolTime;

    private void Start()
    {
        _skill2DurationUI.SetActive(false);
        _skill2DurationBar.fillAmount = 0f;
    }

    public void UseSkill1()
    {
        if (_canUseSkill1 == false)
        {
            Debug.Log("스킬 1 쿨타임 중");
            return;
        }

        _canUseSkill1 = false;

        _skill1Button.interactable = false;
        _skill1CoolTimeImage.gameObject.SetActive(true);
        _skill1CoolTimeText.gameObject.SetActive(true);

        float skillDamage = _hero._playerDamage * _skill1DamageMultiple;

        Instantiate(_skill1Particle, _skill1ParticlePoint.position, Quaternion.identity);

        if (_session.CurrentEnemy != null)
        {
            _hero.SkillAttack(_session.CurrentEnemy, skillDamage);
        }
        else
        {
            _hero.BossSkillAttack(_session.CurrentBoss, skillDamage);
        }

        StartCoroutine(Skill1CoolTimeRoutine());
    }
    IEnumerator Skill1CoolTimeRoutine()
    {
        float remainTime = _skill1CoolTime;

        while (remainTime > 0)
        {
            _skill1CoolTimeText.text = remainTime.ToString("0");

            remainTime -= Time.deltaTime;

            yield return null;
        }

        _canUseSkill1 = true;
        _skill1Button.interactable = true;
        _skill1CoolTimeImage.gameObject.SetActive(false);
        _skill1CoolTimeText.gameObject.SetActive(false);
    }
    public void IncreaseSkill1DamageMultiple(float amount)
    {
        _skill1DamageMultiple += amount;
    }

    public void UseSkill2()
    {
        if (_canUseSkill2 == false)
        {
            Debug.Log("스킬 2 쿨타임 중");
            return;
        }

        if (_isSkill2Active == true)
        {
            Debug.Log("스킬 2가 이미 적용 중입니다.");
            return;
        }

        StartCoroutine(Skill2Routine());
    }

    IEnumerator Skill2Routine()
    {
        _canUseSkill2 = false;
        _isSkill2Active = true;

        _skill2Button.interactable = false;
        _skill2CoolTimeImage.gameObject.SetActive(true);
        _skill2CoolTimeText.gameObject.SetActive(true);

        _skill2DurationUI.SetActive(true);
        _skill2DurationBar.fillAmount = 1f;

        _skill2DurationBar.DOKill();
        _skill2DurationBar.DOFillAmount(0f, _skill2Duration)
            .SetEase(Ease.Linear);

        _hero.SetDamageBuff(_skill2DamageMultiple);

        Debug.Log("스킬 2 사용: 데미지 증가");

        float coolTimeRemainTime = _skill2CoolTime;
        float durationRemainTime = _skill2Duration;

        while (coolTimeRemainTime > 0)
        {
            _skill2CoolTimeText.text = coolTimeRemainTime.ToString("0");

            coolTimeRemainTime -= Time.deltaTime;

            if (_isSkill2Active == true)
            {
                durationRemainTime -= Time.deltaTime;

                if (durationRemainTime <= 0)
                {
                    _hero.ResetDamageBuff(_skill2DamageMultiple);
                    _isSkill2Active = false;

                    _skill2DurationBar.DOKill();
                    _skill2DurationBar.fillAmount = 0f;
                    _skill2DurationUI.SetActive(false);
                }
            }

            yield return null;
        }

        if (_isSkill2Active == true)
        {
            _hero.ResetDamageBuff(_skill2DamageMultiple);
            _isSkill2Active = false;
        }

        _skill2DurationBar.DOKill();
        _skill2DurationBar.fillAmount = 0f;
        _skill2DurationUI.SetActive(false);

        _canUseSkill2 = true;
        _skill2Button.interactable = true;
        _skill2CoolTimeImage.gameObject.SetActive(false);
        _skill2CoolTimeText.gameObject.SetActive(false);

        Debug.Log("스킬 2 쿨타임 종료");
    }

    public void IncreaseSkill2Duration(float amount)
    {
        _skill2Duration += amount;
    }

    public void DecreaseSkill2CoolTime(float amount)
    {
        _skill2CoolTime = Mathf.Max(10f, _skill2CoolTime - amount);
    }

    public void SetSkill1Unlocked(bool isUnlocked)
    {
        _skill1Button.gameObject.SetActive(isUnlocked);
    }

    public void SetSkill2Unlocked(bool isUnlocked)
    {
        _skill2Button.gameObject.SetActive(isUnlocked);
    }
}