using UnityEngine;

public class HeroRenderer : MonoBehaviour
{
    [SerializeField] Animator _animator;

    public void Attack()
    {
        _animator.SetTrigger("OnAttack");
    }
}
