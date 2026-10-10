using UnityEngine;

namespace ProjectSchool2026
{
    public class PlayerAnimation : MonoBehaviour
    {
        private static readonly int _runRight = Animator.StringToHash("runRight");
        private static readonly int _runDown = Animator.StringToHash("runDown");
        private static readonly int _runUp = Animator.StringToHash("runUp");
        private static readonly int _idle = Animator.StringToHash("idle");
        private SpriteRenderer _spriteRenderer;
        private Animator _animator;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _animator = GetComponent<Animator>();
        }

        public void AnimateMovement(Vector2 inputDirection)
        {
            if (inputDirection == Vector2.zero)
            {
                _animator.SetTrigger(_idle);
                return;
            }
            
            if (inputDirection.x != 0)
            {
                _spriteRenderer.flipX = inputDirection.x < 0;
                _animator.SetTrigger(_runRight);
            }
            else if (inputDirection.y != 0)
            {
                _animator.SetTrigger(inputDirection.y < 0 ? _runDown : _runUp);
            }
        }
    }
}