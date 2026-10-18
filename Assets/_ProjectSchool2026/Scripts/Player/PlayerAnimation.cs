using DG.Tweening;
using UnityEngine;

namespace ProjectSchool2026.Player
{
    public class PlayerAnimation : MonoBehaviour
    {
        [Header("Scale Animation")]
        [SerializeField] private Vector3 _scaleOffsetAnimation = new Vector3(0.85f, 0.85f, 0.85f);
        [SerializeField] private float _scaleAnimateDuration = 0.85f;
        
        private static readonly int _runRight = Animator.StringToHash("runRight");
        private static readonly int _runDown = Animator.StringToHash("runDown");
        private static readonly int _runUp = Animator.StringToHash("runUp");
        private static readonly int _idle = Animator.StringToHash("idle");
        
        private SpriteRenderer _spriteRenderer;
        private Animator _animator;
        
        private Sequence _idleSequence;

        private Vector3 _initialScale;

        private void Awake()
        {
            _initialScale = transform.localScale;
            
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _animator = GetComponent<Animator>();

            _idleSequence = DOTween.Sequence()
                .Append(transform.DOScale(_scaleOffsetAnimation, _scaleAnimateDuration))
                .Append(transform.DOScale(_initialScale, _scaleAnimateDuration))
                .SetLoops(-1, LoopType.Yoyo);
            
        }

        public void AnimateMovement(Vector2 inputDirection)
        {
            if (inputDirection == Vector2.zero)
            {
                _animator.SetTrigger(_idle);
                
                if (!_idleSequence.IsPlaying())
                {
                    _idleSequence.Play();
                }
                
                return;
            }
            
            _idleSequence.Pause();
            transform.localScale = _initialScale;
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