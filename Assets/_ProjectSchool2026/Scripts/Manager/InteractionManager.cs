using System.Collections.Generic;
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
        private readonly HashSet<string> _collectedArtifactIds = new HashSet<string>();
        private string _selectedArtifactId;
        private string _worldSceneName = "TileMap";
        private Vector3 _returnPosition;
        private bool _hasReturnPosition;
        private bool _challengeWon;

        public static InteractionManager Instance;
        public bool HasSelectedArtifact => !string.IsNullOrEmpty(_selectedArtifactId);

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                // При возвращении в мир берём ссылки из заново загруженной сцены.
                Instance.BindWorld(_challengeView, _player);
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
            BindWorld(_challengeView, _player);
        }

        private void BindWorld(PopUpView challengeView, Player.Player player)
        {
            _challengeView = challengeView;
            _player = player;
            _currentArtifactItem = null;
            _challengeView?.gameObject.SetActive(false);

            if (_hasReturnPosition && _player != null)
            {
                _player.transform.position = _returnPosition;
                _player.AllowMovement();
                _hasReturnPosition = false;
            }
        }

        public void InteractWithArtifact(ArtifactItem artifactItem)
        {
            if (_currentArtifactItem == null && !IsArtifactCollected(artifactItem.ArtifactId))
            {
                _challengeView?.EnableView();
                _currentArtifactItem = artifactItem;
            }
        }

        public void ApplyArtifactChallenge()
        {
            if (_currentArtifactItem == null)
            {
                return;
            }

            // Между сценами сохраняем ID и позицию, а не ссылку на старый предмет.
            _selectedArtifactId = _currentArtifactItem.ArtifactId;
            _worldSceneName = SceneManager.GetActiveScene().name;
            _returnPosition = _player.transform.position;
            _hasReturnPosition = true;
            _challengeWon = false;
            string sceneName = _currentArtifactItem.SceneName;

            _currentArtifactItem = null;
            _challengeView = null;
            _player = null;
            SceneManager.LoadScene(sceneName);
        }

        public void CancelInteraction()
        {
            _challengeView?.DisableView();
            _player?.AllowMovement();
            _currentArtifactItem = null;
        }

        public bool IsArtifactCollected(string artifactId)
        {
            return _collectedArtifactIds.Contains(artifactId);
        }

        public void SetChallengeResult(bool won)
        {
            _challengeWon = won && HasSelectedArtifact;
        }

        public void CollectCurrentArtifact()
        {
            if (!_challengeWon || !HasSelectedArtifact)
            {
                return;
            }

            _collectedArtifactIds.Add(_selectedArtifactId);
            ReturnToWorld();
        }

        public void ReturnToWorld()
        {
            _selectedArtifactId = null;
            _challengeWon = false;
            SceneManager.LoadScene(_worldSceneName);
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
