using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Skill : MonoBehaviour
{
    [SerializeField] Hero _hero;
    [SerializeField] Session _session;

    [SerializeField] float _skill1DamageMultiple = 5f;

    [SerializeField] ParticleSystem _skill1Particle;
    [SerializeField] Transform _skill1ParticlePoint;

    [SerializeField] float _skill1CoolTime;    
    bool _canUseSkill1 = true;

    [SerializeField] Button _skill1Button;
    [SerializeField] Image _skill1CoolTimeImage;
    [SerializeField] TMP_Text _skill1CoolTimeText;

    public float Skill1DamageMultiple => _skill1DamageMultiple;

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
        Debug.Log("스킬 2 사용");
    }
}