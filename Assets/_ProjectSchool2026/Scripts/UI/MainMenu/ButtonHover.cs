using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ProjectSchool2026.UI.MainMenu
{
	public class ButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
	{
		[SerializeField] private float _hoverScale = 1.08f;
		[SerializeField] private float _animationDuration = 0.15f;

		private Vector3 _initialScale;

		private void Awake()
		{
			_initialScale = transform.localScale;
		}

		public void OnPointerEnter(PointerEventData eventData)
		{
			transform.DOKill();
			transform.DOScale(_initialScale * _hoverScale, _animationDuration);
		}

		public void OnPointerExit(PointerEventData eventData)
		{
			transform.DOKill();
			transform.DOScale(_initialScale, _animationDuration);
		}

		private void OnDisable()
		{
			transform.DOKill();
			transform.localScale = _initialScale;
		}
	}
}
