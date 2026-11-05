using UnityEngine;

namespace ProjectSchool2026.Artifact
{
	public class ArtifactItem : MonoBehaviour
	{
		[SerializeField] private string _sceneName = "ChallengeScene";
		[SerializeField] private string _artifactName;
        [SerializeField] private string _artifactDescription;

        public string SceneName => _sceneName;
        public string ArtifactName => _artifactName;
        public string ArtifactDescription => _artifactDescription;
    }
}
