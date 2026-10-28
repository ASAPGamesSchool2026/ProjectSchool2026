using UnityEngine;

namespace ProjectSchool2026.Artifact
{
	public class ArtifactItem : MonoBehaviour
	{
		[SerializeField] private string _sceneName = "ChallengeScene";
		[SerializeField] private string _artifactName = "Vay";
		[SerializeField] private string _artifactInfo = "Я Vay";

		public string SceneName => _sceneName;
		public string ArtifactName => _artifactName;
		public string АrtifactInfo => _artifactInfo;
		
	}
}
