using ProjectSchool2026.Challenge.Bootstrap;
using UnityEngine;

namespace ProjectSchool2026.Challenge.UI
{
    public class HintsUI : MonoBehaviour
    {
        [SerializeField] private ChallengeBootstrap _bootstrap;
        
        [SerializeField] private RectTransform[] _hintsToDisableAfterStart;
        [SerializeField] private RectTransform[] _hintsToEnableAfterStart;
        
        private void Awake()
        {
            _bootstrap.OnChallengeStarted += DisableAndEnableHints;
        }

        private void DisableAndEnableHints()
        {
            foreach (var hintToDisable in _hintsToDisableAfterStart)
            {
                hintToDisable.gameObject.SetActive(false);
            }

            foreach (var hintToEnable in _hintsToEnableAfterStart)
            {
                hintToEnable.gameObject.SetActive(true);
            }
        }
    }
}