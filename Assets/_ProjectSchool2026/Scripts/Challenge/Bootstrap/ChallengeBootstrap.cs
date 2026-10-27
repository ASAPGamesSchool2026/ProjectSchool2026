using UnityEngine;
using UnityEngine.InputSystem;
using ProjectSchool2026.Challenge.Behaviour;

namespace ProjectSchool2026.Challenge.Bootstrap
{
    public class ChallengeBootstrap : MonoBehaviour
    {
        [SerializeField] private BootBehaviour[] _bootObjects;

        private bool _isChallengeStarted;

        private void Awake()
        {
            AppendAllBootObjects();
        }

        public void Click(InputAction.CallbackContext context)
        {
            if (!_isChallengeStarted)
            {
                _isChallengeStarted = true;

                foreach (BootBehaviour bootObject in _bootObjects)
                {
                    bootObject.Boot();
                }
            }
        }

        private void AppendAllBootObjects()
        {
            BootBehaviour[] newBootObjects = FindObjectsByType<BootBehaviour>();

            if (_bootObjects.Length != newBootObjects.Length)
            {
                _bootObjects = newBootObjects;
            }
        }

        private void OnValidate()
        {
            AppendAllBootObjects();
        }
    }
}