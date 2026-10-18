using UnityEngine;

namespace ProjectSchool2026.Manager
{
    public class InteractionManager : MonoBehaviour
    {
        [SerializeField] private GameObject _challengeView;

        private Player.Player _player;
        
        public static InteractionManager Instance;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);

               _challengeView?.SetActive(false);
                return;
            }
            Destroy(gameObject);
        }

        public void InteractWithArtifact(Player.Player player)
        {
            _player = player;
            if (_player != null)
            {
                _challengeView?.SetActive(true);
            }
        }

        public void CancelInteraction()
        {
            _challengeView?.SetActive(false);
            if (_player != null)
            {
                _player.AllowMovement();
                _player = null;
            }
        }
    }
}