using System.Collections;
using DG.Tweening;
using ProjectSchool2026.Manager;
using TMPro;
using UnityEngine;

namespace _ProjectSchool2026.UI.PopUp
{
	public class PopUpView : MonoBehaviour
	{
		[Header("UI")]
		[SerializeField] private TMP_Text _artifactNameText;
		[SerializeField] private TMP_Text _gameDescriptionText;

		[Header("Animation")]
		[SerializeField] private float _animationDuration = 0.5f;

		private WaitForSeconds _animationDurationWait;

		private void Awake()
		{
			_animationDurationWait = new WaitForSeconds(_animationDuration);
		}

		public void SetInfo(string artifactName, string gameDescription)
		{
			_artifactNameText.text = artifactName;
			_gameDescriptionText.text = gameDescription;
		}

		public void EnableView()
		{
			gameObject.SetActive(true);
			transform.localScale = Vector3.zero;
			transform.DOScale(Vector3.one, _animationDuration).SetLink(gameObject);
		}

		public void DisableView()
		{
			StartCoroutine(DisableViewRoutine());
		}

		// После возвращения из испытания используем сохранённый менеджер.
		public void ApplyArtifactChallenge()
		{
			InteractionManager.Instance?.ApplyArtifactChallenge();
		}

		public void CancelInteraction()
		{
			InteractionManager.Instance?.CancelInteraction();
		}

		private IEnumerator DisableViewRoutine()
		{
			transform.DOScale(Vector3.zero, _animationDuration).SetLink(gameObject);
			yield return _animationDurationWait;
			gameObject.SetActive(false);
		}
	}
}
