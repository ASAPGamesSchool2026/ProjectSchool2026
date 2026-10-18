using UnityEngine;

namespace ProjectSchool2026.Artifact
{
	public class ArtifactItem : MonoBehaviour
	{
		[SerializeField] private string _sceneName = "ChallengeScene";

		public string SceneName => _sceneName;
		
	}
}
