using UnityEngine;

namespace ProjectSchool2026
{
	public class CameraFollow : MonoBehaviour
	{
		[SerializeField] private Transform _target;
		[SerializeField][Min(0f)] private float _smoothSpeed = 8f;
		[SerializeField] private Vector3 _offset = new Vector3(0f, 0f, -10f);

		private void LateUpdate()
		{
			if (!_target) { return; }
			
			var desiredPosition = _target.position + _offset;
			transform.position = Vector3.Lerp(
				transform.position,
				desiredPosition, 
				_smoothSpeed * Time.deltaTime
				);
		}
	}
}
