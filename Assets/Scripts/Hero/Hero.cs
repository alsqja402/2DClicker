using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// 공격, 이동 모션
/// </summary>
public class Hero : MonoBehaviour
{
    [Header("컴포넌트")]
    [SerializeField] HeroModel _model;
    [SerializeField] HeroRenderer _renderer;

    public float _playerDamage = 10;
    public float _CriMultiple = 1.5f; 
    public float _CriPercent = 5.0f;

    public void Attack(Enemy enemy)
    {   
        enemy.TakeHit(_playerDamage);

        _renderer.Attack();

        Debug.Log("Hero Attack!");
    }

    public void DamageUp()
    {
        _playerDamage += 10;
    }

    public void IncreaseDamage(float amount)
    {
        _playerDamage += amount;
    }
    public void IncreaseCriMultiple(float amount)
    {
        _CriMultiple += amount;
    }
    public void IncreaseCriPercent(float amount)
    {
        _CriPercent += amount;
    }
}
