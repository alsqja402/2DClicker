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

    public float playerDamage = 10;

    public void Attack(Enemy enemy)
    {   
        enemy.TakeHit(playerDamage);

        _renderer.Attack();

        Debug.Log("Hero Attack!");
    }

}
