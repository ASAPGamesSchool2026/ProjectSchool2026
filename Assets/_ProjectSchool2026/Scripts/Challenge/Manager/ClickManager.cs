using System;
using ProjectSchool2026.Challenge.Behaviour;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ProjectSchool2026.Challenge.Manager
{
    public class ClickManager : BootBehaviour
    {
        [SerializeField] private ClickableBehaviour[] _clickableBehaviours;
        
        public event Action OnClick;

        private bool _isClickAvailable = false;
        
        public void Click(InputAction.CallbackContext context)
        {
            if (_isClickAvailable)
            {
                OnClick?.Invoke();
            }
        }

        public override void Boot()
        {
            AppendAllClickableObjects();

            foreach (ClickableBehaviour clickableObject in _clickableBehaviours)
            {
                OnClick += clickableObject.ClickHandler;
            }
            
            _isClickAvailable = true;
        }

        private void AppendAllClickableObjects()
        {
            ClickableBehaviour[] newClickableBehaviours = FindObjectsByType<ClickableBehaviour>();

            if (newClickableBehaviours.Length != _clickableBehaviours.Length)
            {
                _clickableBehaviours = newClickableBehaviours;
            }
        }

        private void OnValidate()
        {
            AppendAllClickableObjects();
        }

        private void OnDestroy()
        {
            foreach (ClickableBehaviour clickableObject in _clickableBehaviours)
            {
                OnClick -= clickableObject.ClickHandler;
            }
            
            _isClickAvailable = false;
        }
    }
}