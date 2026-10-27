using ProjectSchool2026.Challenge.Bootstrap;
using UnityEngine;

namespace ProjectSchool2026.Challenge.UI
{
    public class HintsUI : BootBehaviour
    {
        [SerializeField] private RectTransform[] _hintsToDisableAfterStart;
        [SerializeField] private RectTransform[] _hintsToEnableAfterStart;

        public override void Boot(ChallengeBootstrap bootstrap)
        {
            DisableAndEnableHints();
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