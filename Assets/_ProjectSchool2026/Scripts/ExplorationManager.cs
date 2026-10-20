using System.Collections.Generic;
using UnityEngine;

namespace ProjectSchool2026
{
	public class ExplorationManager : MonoBehaviour
	{
		[SerializeField] private List<ArtifactItem> _requiredArtifacts = new List<ArtifactItem>();
		[SerializeField] private ArtifactCounterView _counterView;
		[SerializeField] private GameObject _victoryMessage;

		private readonly HashSet<ArtifactItem> _collectedArtifacts = new HashSet<ArtifactItem>();

		public static ExplorationManager Instance { get; private set; }

		public int CollectedArtifactCount => _collectedArtifacts.Count;

		public int TotalArtifactCount => _requiredArtifacts.Count;

		private void Awake()
		{
			if (Instance != null && Instance != this)
			{
				Destroy(gameObject);
				return;
			}

			Instance = this;
		}

		private void Start()
		{
			if (_victoryMessage != null)
			{
				_victoryMessage.SetActive(false);
			}

			UpdateCounter();
		}

		public bool CollectArtifact(ArtifactItem artifact)
		{
			if (artifact == null || !_requiredArtifacts.Contains(artifact))
			{
				return false;
			}

			// Повторно собранный предмет не добавляется в набор.
			if (!_collectedArtifacts.Add(artifact))
			{
				return false;
			}

			UpdateCounter();

			if (_victoryMessage != null && AreAllArtifactsCollected())
			{
				_victoryMessage.SetActive(true);
			}

			return true;
		}

		public bool AreAllArtifactsCollected()
		{
			if (_requiredArtifacts.Count == 0)
			{
				return false;
			}

			foreach (var artifact in _requiredArtifacts)
			{
				if (artifact == null || !_collectedArtifacts.Contains(artifact))
				{
					return false;
				}
			}

			return true;
		}

		private void UpdateCounter()
		{
			if (_counterView != null)
			{
				_counterView.ShowProgress(CollectedArtifactCount, TotalArtifactCount);
			}
		}

		private void OnDestroy()
		{
			if (Instance == this)
			{
				Instance = null;
			}
		}
	}
}
