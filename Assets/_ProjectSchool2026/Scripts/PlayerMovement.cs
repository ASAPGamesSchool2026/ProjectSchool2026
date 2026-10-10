using UnityEngine;
using UnityEngine.InputSystem;

namespace ProjectSchool2026
{
	[RequireComponent(typeof(Rigidbody2D))]
	public class PlayerMovement : MonoBehaviour
	{
		[Header("Player Settings")]
		[SerializeField] private Rigidbody2D _rigidbody;
		[SerializeField] private PlayerAnimation _playerAnimation;
		
		[Header("Player Parametrs")]
		[SerializeField][Min(0f)] private float _moveSpeed = 4f;
		
		private Vector2 _inputDirection;

		private void Awake()
		{
			if (_rigidbody == null)
			{
				_rigidbody = GetComponent<Rigidbody2D>();
			}

			if (_playerAnimation == null)
			{
				_playerAnimation = GetComponentInChildren<PlayerAnimation>();
			}

			_rigidbody.gravityScale = 0f;
			_rigidbody.freezeRotation = true;
		}

		public void OnMove(InputAction.CallbackContext context)
		{
			_inputDirection = context.ReadValue<Vector2>();
			_playerAnimation.AnimateMovement(_inputDirection);
		}

		private void FixedUpdate()
		{
			_rigidbody.linearVelocity = _inputDirection.normalized * _moveSpeed;
		}

		private void OnDisable()
		{
			_inputDirection = Vector2.zero;

			if (_rigidbody != null)
			{
				_rigidbody.linearVelocity = Vector2.zero;
			}
		}
	}
}
