using System.Collections.Generic;
using _ProjectSchool2026.UI.PopUp;
using ProjectSchool2026.Artifact;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ProjectSchool2026.Manager
{
    public class InteractionManager : MonoBehaviour
    {
        [Header("Scenes")]
        [SerializeField] private string _mapSceneName = "TileMap";

        [Header("Start PopUp")]
        [SerializeField] private string _startButtonText = "Начать";

        [Header("Result PopUp")]
        [SerializeField] private string _winTitle = "Победа!";
        [SerializeField, TextArea(2, 4)] private string _winDescription = "Ты набрал 100 очков. Артефакт твой!";
        [SerializeField] private string _loseTitle = "Время вышло";
        [SerializeField, TextArea(2, 4)] private string _loseDescription = "Очков не хватило." +
                                                                           " Попробуй пройти испытание ещё раз.";
        [SerializeField] private string _backButtonText = "Вернуться";

        public static InteractionManager Instance { get; private set; }

        private readonly HashSet<string> _completedArtifactIds = new HashSet<string>();

        private string _currentArtifactId;
        private string _currentChallengeSceneName;

        private Player.Player _player;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
                return;
            }
            Destroy(gameObject);
        }

        public bool IsArtifactCompleted(string artifactId)
        {
            return _completedArtifactIds.Contains(artifactId);
        }

        public void InteractWithArtifact(Player.Player player, ArtifactItem artifactItem)
        {
            if (_currentArtifactId != null)
            {
                return;
            }

            _player = player;
            _currentArtifactId = artifactItem.ArtifactId;
            _currentChallengeSceneName = artifactItem.SceneName;

            PopUpView.Instance.Show(artifactItem.ArtifactName, artifactItem.GameDescription, _startButtonText,
                ApplyArtifactChallenge, CancelInteraction);
        }

        private void ApplyArtifactChallenge()
        {
            PopUpView.Instance.Hide();
            SceneManager.LoadScene(_currentChallengeSceneName);
        }

        private void CancelInteraction()
        {
            PopUpView.Instance.Hide();

            if (_player != null)
            {
                _player.AllowMovement();
            }

            _player = null;
            _currentArtifactId = null;
        }

        public void FinishChallenge(bool isWin)
        {
            if (isWin && _currentArtifactId != null)
            {
                _completedArtifactIds.Add(_currentArtifactId);
            }

            string title = isWin ? _winTitle : _loseTitle;
            string description = isWin ? _winDescription : _loseDescription;

            PopUpView.Instance.Show(title, description, _backButtonText, ReturnToMap);
        }

        private void ReturnToMap()
        {
            PopUpView.Instance.Hide();

            _player = null;
            _currentArtifactId = null;

            SceneManager.LoadScene(_mapSceneName);
        }
    }
}
