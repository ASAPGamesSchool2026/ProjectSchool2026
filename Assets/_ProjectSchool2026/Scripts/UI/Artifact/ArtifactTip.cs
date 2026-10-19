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
            
        }


        private void OnTriggerEnter2D(Collider2D other)
        {
            
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            
        }
    }
}
