using DG.Tweening;
using ProjectSchool2026.Player;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectSchool2026.UI.Artifact
{
    public class ArtifactTip : MonoBehaviour
    {
        [Header("UI")]
        [SerializeField] private Canvas _canvas;
        [SerializeField] private Image _imageTip;

        [Header("Animation")]
        [SerializeField] private Vector3 _animationScale = new(0.85f, 0.85f, 0.85f);
        [SerializeField] private float animationDuration = 0.5f;
        
        private Sequence _sequence;
        private Vector3 _originalScale;

        private void Awake()
        {
            if (_canvas != null)
            {
                _canvas.gameObject.SetActive(false);
            }

            if (_imageTip != null)
            {
                _originalScale = _imageTip.transform.localScale;
                
                _sequence = DOTween.Sequence()
                    .Append(_imageTip.transform.DOScale(_animationScale, animationDuration))
                    .Append(_imageTip.transform.DOScale(_originalScale, animationDuration))
                    .SetLoops(-1, LoopType.Yoyo);
            }
        }


        private void OnTriggerEnter2D(Collider2D other)
        {
            PlayerMovement player = other.GetComponent<PlayerMovement>();

            if (player != null)
            {
                _canvas.gameObject.SetActive(true);

                if (!_sequence.IsPlaying())
                {
                    _sequence.Play();
                }
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            PlayerMovement player = other.GetComponent<PlayerMovement>();
            if (player != null)
            {
                _sequence.Pause();
                _canvas.gameObject.SetActive(false);
                _imageTip.transform.localScale = _originalScale;
            }
        }
    }
}