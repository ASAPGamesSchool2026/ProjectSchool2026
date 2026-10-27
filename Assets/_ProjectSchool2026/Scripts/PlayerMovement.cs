using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] [Min(0f)] private float _moveSpeed = 4f;
    [SerializeField] private Rigidbody2D _rigidbody2D;
    
    private Vector3 _inputDirection;

    private void Awake()
    {
        if (_rigidbody2D == null)
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }
        
        _rigidbody2D.gravityScale = 0f;
        _rigidbody2D.freezeRotation = true;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        _inputDirection = context.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        _rigidbody2D.linearVelocity = _inputDirection.normalized * _moveSpeed;
    }

    private void OnDisable()
    {
        _inputDirection = Vector2.zero;

        if (_rigidbody2D != null)
        {
            _rigidbody2D.linearVelocity = Vector2.zero;
        }
    }
}
