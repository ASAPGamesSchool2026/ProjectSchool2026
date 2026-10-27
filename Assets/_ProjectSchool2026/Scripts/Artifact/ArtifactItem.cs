using ProjectSchool2026.Manager;
using UnityEngine;

namespace ProjectSchool2026.Artifact
{
	public class ArtifactItem : MonoBehaviour
	{
		[SerializeField] private string _artifactId = "artifact_01";
		[SerializeField] private string _sceneName = "Challenge";
		[SerializeField] private string _artifactName = "Артефакт";
		[SerializeField][TextArea(2, 4)] private string _gameDescription;

		public string ArtifactId => _artifactId;
		public string SceneName => _sceneName;
		public string ArtifactName => _artifactName;
		public string GameDescription => _gameDescription;

		private void Start()
		{
			if (InteractionManager.Instance != null && InteractionManager.Instance.IsArtifactCompleted(_artifactId))
			{
				gameObject.SetActive(false);
			}
		}
	}
}
