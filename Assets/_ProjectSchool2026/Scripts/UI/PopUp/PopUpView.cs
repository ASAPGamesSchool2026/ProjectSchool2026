using System;
using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _ProjectSchool2026.UI.PopUp
{
	public class PopUpView : MonoBehaviour
	{
		public static PopUpView Instance { get; private set; }

		[Header("UI")]
		[SerializeField] private TMP_Text _titleText;
		[SerializeField] private TMP_Text _descriptionText;

		[Header("Buttons")]
		[SerializeField] private Button _confirmButton;
		[SerializeField] private TMP_Text _confirmButtonText;
		[SerializeField] private Button _cancelButton;

		[Header("Animation")]
		[SerializeField] private float _animationDuration = 0.5f;

		private WaitForSeconds _animationDurationWait;

		// Что делать при нажатии на кнопки. Поп-ап сам этого не знает — это ему передают снаружи
		private Action _onConfirm;
		private Action _onCancel;

		private void Awake()
		{
			// Поп-ап лежит внутри Canvas, а между сценами можно переносить только корневой объект,
			// поэтому сохраняем (или удаляем) весь Canvas целиком
			GameObject canvasObject = transform.root.gameObject;

			if (Instance != null)
			{
				Destroy(canvasObject);
				return;
			}

			Instance = this;
			DontDestroyOnLoad(canvasObject);

			_animationDurationWait = new WaitForSeconds(_animationDuration);
			_confirmButton.onClick.AddListener(Confirm);
			_cancelButton.onClick.AddListener(Cancel);
			gameObject.SetActive(false);
		}

		// onCancel можно не передавать — тогда кнопка отмены будет скрыта
		public void Show(string title, string description, string confirmText, Action onConfirm, Action onCancel = null)
		{
			_titleText.text = title;
			_descriptionText.text = description;
			_confirmButtonText.text = confirmText;

			_onConfirm = onConfirm;
			_onCancel = onCancel;
			_cancelButton.gameObject.SetActive(onCancel != null);

			gameObject.SetActive(true);
			transform.localScale = Vector3.zero;
			transform.DOScale(Vector3.one, _animationDuration).SetLink(gameObject);
		}

		public void Hide()
		{
			if (gameObject.activeSelf)
			{
				StartCoroutine(HideRoutine());
			}
		}

		private void Confirm()
		{
			_onConfirm?.Invoke();
		}

		private void Cancel()
		{
			_onCancel?.Invoke();
		}

		private IEnumerator HideRoutine()
		{
			transform.DOScale(Vector3.zero, _animationDuration).SetLink(gameObject);
			yield return _animationDurationWait;
			gameObject.SetActive(false);
		}
	}
}
