using UnityEngine;

public class playerAnimation : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    public void PlayerAnimation(Vector2 inputDirection)
    {
        if (inputDirection == Vector2.zero)
        {
            _animator.SetTrigger("Idle");
        }
        
        if (inputDirection.x != 0)
        {
            _spriteRenderer.flipX = inputDirection.x < 0;
            _animator.SetTrigger("Right");
        }
        else if (inputDirection.y != 0)
        {
            _animator.SetTrigger(inputDirection.y > 0 ? "runUp" : "runDouwn");
        }
    }
}
