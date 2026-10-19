using UnityEngine;
using UnityEngine.InputSystem;

namespace ProjectSchool2026.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] Player _player;
        
        private Vector2 _inputDirection;
        private bool _isInteracting;
        
        public void OnMove(InputAction.CallbackContext context)
        {
            _inputDirection = context.ReadValue<Vector2>();
            _player.SetInputDirection(_inputDirection);
        }

        public void OnInteract(InputAction.CallbackContext context)
        {
        }
    }
}