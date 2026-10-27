using ProjectSchool2026.Challenge.Bootstrap;
using ProjectSchool2026.Challenge.Manager;
using ProjectSchool2026.Input;
using UnityEngine;

namespace ProjectSchool2026.Challenge
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private ChallengeBootstrap _bootstrap;
        [SerializeField] private ClickManager _clickManager;
        
        private InputSystemActions _inputAction;

        private void Awake()
        {
            _inputAction = new InputSystemActions();
            _inputAction.Enable();
            _inputAction.UI.Click.started += _clickManager.Click;
            _inputAction.UI.Click.started += _bootstrap.Click;
        }

        private void OnDestroy()
        {
            _inputAction.UI.Click.started -= _clickManager.Click;
            _inputAction.UI.Click.started -= _bootstrap.Click;
            _inputAction.Disable();
        }
    }
}