using _ProjectSchool2026.UI.PopUp;
using ProjectSchool2026.Artifact;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ProjectSchool2026.Manager
{
    public class InteractionManager : MonoBehaviour
    {
        [SerializeField] private PopUpView _challengeView;
        [SerializeField] private Player.Player _player;
        
        private ArtifactItem _currentArtifactItem;
        
        public static InteractionManager Instance;

        private void Awake()
        {
        }

        public void InteractWithArtifact(ArtifactItem artifactItem)
        {
        }

        public void ApplyArtifactChallenge()
        {
        }

        public void CancelInteraction()
        {
        }

        
    }
}