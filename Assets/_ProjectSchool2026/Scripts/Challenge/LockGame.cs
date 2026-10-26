using ProjectSchool2026.Manager;
using ProjectSchool2026.UI.Challenge;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ProjectSchool2026.Challenge
{
    public class LockGame : MonoBehaviour
    {
        [SerializeField] private LockGameView _view;

        [Header("Правила")]
        [SerializeField] private int _targetScore = 100;
        [SerializeField] private float _roundTime = 20f;
        [SerializeField] private int _yellowPoints = 10;
        [SerializeField] private int _bluePoints = 20;
        [SerializeField] private float _timeBonus = 3f;
        [SerializeField] private float _missPenalty = 2f;

        [Header("Скорость и размеры зон")]
        [SerializeField] private float _startSpeed = 90f;
        [SerializeField] private float _startYellowWidth = 20f;
        [SerializeField] private float _startBlueWidth = 5f;
        [SerializeField] private int _hitsPerLevel = 3;
        [SerializeField] private float _speedMultiplier = 1.05f;
        [SerializeField] private float _widthMultiplier = 0.9f;
        [SerializeField] private float _maxSpeed = 120f;
        [SerializeField] private float _minYellowWidth = 12f;
        [SerializeField] private float _minBlueWidth = 4f;

        private float _arrowAngle;
        private float _yellowAngle;
        private float _blueAngle;
        private float _yellowWidth;
        private float _blueWidth;
        private float _speed;
        private float _timeLeft;
        private int _score;
        private int _combo;
        private int _successfulHits;
        private bool _isPlaying;

        public bool IsWon { get; private set; }

        private void Start()
        {
            StartRound();
        }

        // Сброс попытки и связь с готовым интерфейсом подготовлены до урока.
        public void StartRound()
        {
            _score = 0;
            _combo = 0;
            _successfulHits = 0;
            _arrowAngle = 0f;
            _yellowAngle = 90f;
            _blueAngle = 270f;
            _timeLeft = _roundTime;
            _speed = _startSpeed;
            _yellowWidth = _startYellowWidth;
            _blueWidth = _startBlueWidth;
            _isPlaying = true;
            IsWon = false;

            InteractionManager.Instance?.SetChallengeResult(false);
            MoveYellowZone();
            MoveBlueZone();
            _view.HideResult();
            _view.SetArrow(_arrowAngle);
            RefreshView();
        }

        private void Update()
        {
            if (!_isPlaying)
            {
                return;
            }

            RotateArrow();
            UpdateTimer();
        }

        private void RotateArrow()
        {
            _arrowAngle += _speed * Time.deltaTime;
            _arrowAngle = Mathf.Repeat(_arrowAngle, 360f);
            _view.SetArrow(_arrowAngle);
        }

        private void UpdateTimer()
        {
            _timeLeft = Mathf.Max(0f, _timeLeft - Time.deltaTime);
            _view.SetTime(_timeLeft / _roundTime);

            if (_timeLeft <= 0f)
            {
                FinishRound(false);
            }
        }

        private bool IsInsideZone(float center, float width)
        {
            float difference = Mathf.Abs(Mathf.DeltaAngle(_arrowAngle, center));
            return difference <= width / 2f;
        }

        public void TryHit()
        {
            if (!_isPlaying)
            {
                return;
            }

            if (IsInsideZone(_blueAngle, _blueWidth))
            {
                _timeLeft = Mathf.Min(_roundTime, _timeLeft + _timeBonus);
                RegisterHit(_bluePoints);
                MoveBlueZone();
            }
            else if (IsInsideZone(_yellowAngle, _yellowWidth))
            {
                RegisterHit(_yellowPoints);
                MoveYellowZone();
            }
            else
            {
                Miss();
                return;
            }

            RefreshView();

            if (_score >= _targetScore)
            {
                FinishRound(true);
            }
        }

        private void RegisterHit(int points)
        {
            _score += points;
            _combo++;
            _successfulHits++;
            _view.ShowHit();
            UpdateDifficulty();
        }

        private void Miss()
        {
            _combo = 0;
            _timeLeft = Mathf.Max(0f, _timeLeft - _missPenalty);
            _view.ShowMiss();
            RefreshView();

            if (_timeLeft <= 0f)
            {
                FinishRound(false);
            }
        }

        private void MoveYellowZone()
        {
            _yellowAngle = NewZoneAngle(_blueAngle);
        }

        private void MoveBlueZone()
        {
            _blueAngle = NewZoneAngle(_yellowAngle);
        }

        // Между центрами оставляем минимум 30 градусов, чтобы зоны не пересекались.
        private float NewZoneAngle(float otherAngle)
        {
            return Mathf.Repeat(otherAngle + Random.Range(30f, 330f), 360f);
        }

        private void UpdateDifficulty()
        {
            if (_successfulHits % _hitsPerLevel != 0)
            {
                return;
            }

            _speed = Mathf.Min(_maxSpeed, _speed * _speedMultiplier);
            _yellowWidth = Mathf.Max(_minYellowWidth, _yellowWidth * _widthMultiplier);
            _blueWidth = Mathf.Max(_minBlueWidth, _blueWidth * _widthMultiplier);
        }

        private void RefreshView()
        {
            _view.SetZones(_yellowAngle, _yellowWidth, _blueAngle, _blueWidth);
            _view.SetScore(_score, _targetScore, _combo);
            _view.SetTime(_timeLeft / _roundTime);
        }

        // Окно результата и переходы между сценами подготовлены до урока.
        private void FinishRound(bool won)
        {
            _isPlaying = false;
            IsWon = won;
            InteractionManager.Instance?.SetChallengeResult(won);
            bool hasArtifact = InteractionManager.Instance != null
                && InteractionManager.Instance.HasSelectedArtifact;
            _view.ShowResult(won, hasArtifact, _score);
        }

        public void CollectArtifact()
        {
            if (IsWon)
            {
                InteractionManager.Instance?.CollectCurrentArtifact();
            }
        }

        public void Exit()
        {
            if (InteractionManager.Instance != null)
            {
                InteractionManager.Instance.ReturnToWorld();
                return;
            }

            SceneManager.LoadScene("TileMap");
        }
    }
}
