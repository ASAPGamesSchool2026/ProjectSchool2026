using ProjectSchool2026.Challenge.Bootstrap;
using ProjectSchool2026.Challenge.Input;
using ProjectSchool2026.Scripts.Challenge.Manager;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ProjectSchool2026.Scripts.Challenge
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private Bootstrap _bootstrap;
        [SerializeField] private ClickManager _clickManager;
        
        private InputSystemActions _inputAction;

        private void Awake()
        {
            _inputAction = new InputSystemActions();
            _inputAction.Enable();
            _inputAction.UI.Click.started += OnClicked;
        }

        private void OnDestroy()
        {
            _inputAction.UI.Click.started -= OnClicked;
            _inputAction.Disable();
        }

        private void OnClicked(InputAction.CallbackContext context)
        {
            _clickManager.Click();
            _bootstrap.Click();
        }
    }
}