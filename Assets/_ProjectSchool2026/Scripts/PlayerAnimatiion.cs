using UnityEngine;

public class PlayerAnimatiion : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    public void PlayAnimation(Vector2 inputDirection)
    {
        if (inputDirection == Vector2.zero)
        {
            _animator.SetTrigger("Idle");
        }

        if (inputDirection.x != 0)
        {
            _spriteRenderer.flipX = inputDirection.x < 0;
            _animator.SetTrigger("RunRight");
        }

        if (inputDirection.y > 0)
        {
            _animator.SetTrigger("RunUp");
        }
        else if (inputDirection.y < 0)
        {
            _animator.SetTrigger("RunDown");
        }
    }
}
