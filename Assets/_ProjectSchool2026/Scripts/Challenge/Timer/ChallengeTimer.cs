using ProjectSchool2026.Challenge.Behaviour;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectSchool2026.Challenge.Timer
{
    public class ChallengeTimer : BootBehaviour
    {
        [SerializeField] private Arrow.Arrow _arrow;
        
        [SerializeField] private Image _imageFilling;

        [Header("Timer Settings")] 
        [SerializeField, Range(10f, 100f)] private float _totalTime;
        [SerializeField, Range(0.001f, 100f)] private float _timeStep;
        
        [Header("Bonus Setting")]
        [SerializeField, Range(1f, 100f)] private float _bonusTime;
        [SerializeField, Range(1f, 100f)] private float _penaltyTime;
        
        private float _currentTime;

        private bool _timerRunning;

        public override void Boot()
        {
            _arrow.OnHardZoneSelected += IncreaseTimeForSelection;
            _currentTime = _totalTime;
            StartTimer();
        }

        private void StartTimer()
        {
            _timerRunning = true;
        }

        private void Update()
        {
            if (_timerRunning)
            {
                _currentTime -= _timeStep * Time.deltaTime;
                _imageFilling.fillAmount = _currentTime / _totalTime;

                if (_currentTime <= 0)
                {
                    _timerRunning = false;
                    _currentTime = 0;
                    _imageFilling.fillAmount = 0f;
                }
            }
        }

        private void IncreaseTimeForSelection()
        {
            _currentTime += _bonusTime;

            if (_currentTime >= _totalTime)
            {
                _currentTime = _totalTime;
            }
            _imageFilling.fillAmount = _currentTime / _totalTime;
        }

        private void ReduceTimeForSelection()
        {
            _currentTime -= _penaltyTime;

            if (_currentTime < 0)
            {
                _currentTime = 0;
            }
            
            _imageFilling.fillAmount = _currentTime / _totalTime;
        }
    }
}