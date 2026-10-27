using ProjectSchool2026.Challenge.Behaviour;
using TMPro;
using UnityEngine;

namespace ProjectSchool2026.Challenge.Manager
{
    public class ScoreManager : BootBehaviour
    {
        [Header("TextMesh Pro Settings")]
        [SerializeField] private TMP_Text _scoreText;

        [Header("Scores Settings")] 
        [SerializeField, Range(0, 10)] private uint _scoreForDefaultZone;
        [SerializeField, Range(0, 10)] private uint _scoreForHardZone;
        [SerializeField, Range(0, 10)] private uint _penaltyScores;

        [SerializeField] private Arrow.Arrow _arrow;

        private uint _totalScore;

        public override void Boot()
        {
            if (_arrow != null)
            {
                _arrow.OnHardZoneSelected += AddScoresForHardZone;
                _arrow.OnDefaultZoneSelected += AddScoresForDefaultZone;
                _arrow.OnWhiteZoneSelected += ReduceScores;
            }
            _scoreText.text = $"{_totalScore}";
        }

        private void ReduceScores()
        {
            if ((int)(_totalScore - _penaltyScores) < 0)
            {
                _totalScore = 0;
                _scoreText.text = $"{_totalScore}";
                return;
            }
            
            _totalScore -= _penaltyScores;
            _scoreText.text = $"{_totalScore}";
        }

        private void AddScoresForDefaultZone()
        {
            _totalScore += _scoreForDefaultZone;
            _scoreText.text = $"{_totalScore}";
        }

        private void AddScoresForHardZone()
        {
            _totalScore += _scoreForHardZone;
            _scoreText.text = $"{_totalScore}";
        }


        private void OnDestroy()
        {
            if (_arrow != null)
            {
                _arrow.OnHardZoneSelected -= AddScoresForHardZone;
                _arrow.OnDefaultZoneSelected += AddScoresForDefaultZone;
            }
            
        }
    }
}