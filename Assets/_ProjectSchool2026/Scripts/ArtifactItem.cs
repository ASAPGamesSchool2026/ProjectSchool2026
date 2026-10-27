using UnityEngine;

namespace ProjectSchool2026
{
	public class ArtifactItem : MonoBehaviour
	{
		[SerializeField] private string _artifactName = "Лесной артефакт";

		public string ArtifactName => _artifactName;

		private void OnTriggerEnter2D(Collider2D other)
		{
			if(other.attachedRigidbody == null) { return; }

			if (!other.attachedRigidbody.TryGetComponent<PlayerMovement>(out _))
			{
				return;
			}
			
			var explorationManager = ExplorationManager.Instance;
			if (explorationManager.CollectArtifact(this))
			{
				gameObject.SetActive(false);
			}
		}
	}
}
