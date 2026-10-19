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
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);

               _challengeView?.gameObject.SetActive(false);
                return;
            }
            Destroy(gameObject);
        }

        public void InteractWithArtifact(ArtifactItem artifactItem)
        {
            if (_currentArtifactItem == null)
            {
                _challengeView?.EnableView();
                _currentArtifactItem = artifactItem;
            }
        }

        public void ApplyArtifactChallenge()
        {
            Debug.Log("Applying artifact challenge");
            if (_currentArtifactItem != null)
            {
                SceneManager.LoadScene(_currentArtifactItem.SceneName);
            }
        }

        public void CancelInteraction()
        {
            _challengeView?.DisableView();
            if (_player != null && _currentArtifactItem != null)
            {
                _player.AllowMovement();
                _currentArtifactItem = null;
            }
        }

        
    }
}