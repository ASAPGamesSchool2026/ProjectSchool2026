using ProjectSchool2026.Manager;
using UnityEngine;

namespace ProjectSchool2026.Artifact
{
	public class ArtifactItem : MonoBehaviour
	{
		[SerializeField] private string _artifactId = "artifact_01";
		[SerializeField] private string _sceneName = "Challenge";

		public string ArtifactId => _artifactId;
		public string SceneName => _sceneName;

		private void Start()
		{
			if (InteractionManager.Instance != null
			    && InteractionManager.Instance.IsArtifactCollected(_artifactId))
			{
				gameObject.SetActive(false);
			}
		}
	}
}
