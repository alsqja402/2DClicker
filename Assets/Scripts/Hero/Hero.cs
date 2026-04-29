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
    public float _criMultiple = 1.5f; 
    public float _criPercent = 0.05f;

    public void Attack(Enemy enemy)
    {   
        float finalDamage = _playerDamage * _criMultiple;

        if (Random.value < _criPercent)
        {
            enemy.TakeHit(finalDamage, true);
            Debug.Log("크리");
        }
        else
        {
            enemy.TakeHit(_playerDamage);
        }

        _renderer.Attack();

        Debug.Log("Hero Attack!");
    }

    public void IncreaseDamage(float amount)
    {
        _playerDamage += amount;
    }
    public void IncreaseCriMultiple(float amount)
    {
        _criMultiple += amount;
    }
    public void IncreaseCriPercent(float amount)
    {
        _criPercent += amount;
    }
}
