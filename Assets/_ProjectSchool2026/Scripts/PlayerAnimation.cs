using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] public SpriteRenderer _spriteRenderer;

    public void PlayAnimation(Vector2 inputDirection)
    {
        if (inputDirection == Vector2.zero)
        {
            _animator.SetTrigger("idle");
            return;
        }

        if (inputDirection.x != 0)
        {
            _spriteRenderer.flipX = inputDirection.x > 0;
            _animator.SetTrigger("runLeft");
        }
        else if (inputDirection.y != 0)
        {
            _animator.SetTrigger(inputDirection.y > 0 ? "runUp" : "runDown");
        }
    }
}
