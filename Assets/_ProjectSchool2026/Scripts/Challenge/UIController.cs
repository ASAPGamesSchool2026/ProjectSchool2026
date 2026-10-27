using ProjectSchool2026.Challenge.Bootstrap;
using ProjectSchool2026.Challenge.Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ProjectSchool2026.Scripts.Challenge
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private Bootstrap _bootstrap;

        private InputSystemActions _inputSystem;
        
        private void Awake()
        {
            _inputSystem = new InputSystemActions();
            _inputSystem.Enable();

            _inputSystem.UI.Click.started += OnClicked;
        }

        private void OnClicked(InputAction.CallbackContext context)
        {
            _bootstrap.Click();
        }
    }
}