using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ProjectSchool2026.Challenge.Bootstrap
{
    public class ChallengeBootstrap : MonoBehaviour
    {
        private bool _isChallengeStarted = false;
        
        public event Action OnChallengeStarted;

        public event Action OnClick;
        
        public void Click(InputAction.CallbackContext context)
        {
            if (!_isChallengeStarted)
            {
                _isChallengeStarted = true;
                OnChallengeStarted?.Invoke();
                return;
            }
            
            OnClick?.Invoke();
        }
    }
}