using ProjectSchool2026.Artifact;
using ProjectSchool2026.Manager;
using ProjectSchool2026.UI.Artifact;
using UnityEngine;

namespace ProjectSchool2026.Player
{
	[RequireComponent(typeof(Rigidbody2D))]
	public class Player : MonoBehaviour
	{
		[Header("Player Settings")]
		[SerializeField] private Rigidbody2D _rigidbody;
		[SerializeField] private PlayerAnimation _playerAnimation;
		
		[Header("Player Parametrs")]
		[SerializeField][Min(0f)] private float _moveSpeed = 4f;
		
		private ArtifactItem _currentArtifactItem;
		private Vector2 _inputDirection;
		
		private bool _isInteracting;
		private bool _canInteract;
		private bool _canMove;

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
			_canMove = true;
		}

		public void SetInputDirection(Vector2 inputDirection)
		{
			_inputDirection = inputDirection;
			AnimateByInputDirection(inputDirection);
		}

		public void SetInteracting(bool isInteracting)
		{
			_isInteracting = isInteracting;

			if (_canInteract && _isInteracting)
			{
				InteractionManager.Instance.InteractWithArtifact(this, _currentArtifactItem);
				_canMove = false;
				_rigidbody.linearVelocity = Vector2.zero;
				_playerAnimation.AnimateMovement(Vector2.zero);
			}
		}

		public void AllowMovement()
		{
			_canMove = true;
		}

		private void AnimateByInputDirection(Vector2 inputDirection)
		{
			if (_canMove)
			{
				_playerAnimation.AnimateMovement(inputDirection);
			}
		}
		
		private void FixedUpdate()
		{
			if (_canMove)
			{
				_rigidbody.linearVelocity = _inputDirection.normalized * _moveSpeed;
			}
		}

		private void OnTriggerEnter2D(Collider2D other)
		{
			ArtifactItem artifact = other.GetComponent<ArtifactItem>();
			if (artifact != null)
			{
				_currentArtifactItem = artifact;
				_canInteract = true;
			}
		}

		private void OnTriggerExit2D(Collider2D other)
		{
			ArtifactItem artifact = other.GetComponent<ArtifactItem>();
			if (artifact != null)
			{
				_currentArtifactItem = null;
				_canInteract = false;
			}
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
