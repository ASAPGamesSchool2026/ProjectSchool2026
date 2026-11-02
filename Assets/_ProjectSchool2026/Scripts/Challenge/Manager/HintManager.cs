using ProjectSchool2026.Challenge.Behaviour;
using UnityEngine;

namespace ProjectSchool2026.Scripts.Challenge.Manager
{
    public class HintManager : BootBehaviour
    {
        [SerializeField] private RectTransform[] _hintsToDisableAfterStart;
        [SerializeField] private RectTransform[] _hintsToEnableAfterStart;

        private void DisableAndEnableHints()
        {
            foreach (RectTransform hint in _hintsToDisableAfterStart)
            {
                hint.gameObject.SetActive(false);
            }
            
            foreach (RectTransform hint in _hintsToEnableAfterStart)
            {
                hint.gameObject.SetActive(true);
            }
        }

        public override void Boot()
        {
            DisableAndEnableHints();
        }
    }
}