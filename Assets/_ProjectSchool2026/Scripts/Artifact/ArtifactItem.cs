using ProjectSchool2026.Manager;
using ProjectSchool2026.Player;
using UnityEngine;

namespace ProjectSchool2026.Artifact
{
	public class ArtifactItem : MonoBehaviour
	{
		[SerializeField] private string _artifactName = "Лесной артефакт";

		public string ArtifactName => _artifactName;

		private void OnTriggerEnter2D(Collider2D other)
		{
			if (other.attachedRigidbody == null)
			{
				return;
			}

			if (!other.attachedRigidbody.TryGetComponent<Player.Player>(out _))
			{
				return;
			}

			var explorationManager = ExplorationManager.Instance;
			if (explorationManager == null)
			{
				return;
			}

			if (explorationManager.CollectArtifact(this))
			{
				gameObject.SetActive(value: false);
			}
		}
	}
}
