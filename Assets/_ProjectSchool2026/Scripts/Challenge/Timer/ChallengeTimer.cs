using System;
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

        private bool _IsTimerRunning;

        public event Action OnTimeIsUp;

        public override void Boot()
        {
            _arrow.OnHardZoneSelected += IncreaseTimeForSelection;
            _arrow.OnWhiteZoneSelected += ReduceTimeForSelection;
            _currentTime = _totalTime;
            _imageFilling.fillAmount = 1f;
            StartTimer();
        }

        private void StartTimer()
        {
            _IsTimerRunning = true;
        }

        public void StopTimer()
        {
            _IsTimerRunning = false;
        }

        private void Update()
        {
            if (_IsTimerRunning)
            {
                _currentTime = Mathf.Max(0f, _currentTime - _timeStep * Time.deltaTime);
                _imageFilling.fillAmount = _currentTime / _totalTime;
                CheckTimeIsUp();
            }
        }

        private void IncreaseTimeForSelection()
        {
            if (!_IsTimerRunning)
            {
                return;
            }

            _currentTime = Mathf.Min(_totalTime, _currentTime + _bonusTime);
            _imageFilling.fillAmount = _currentTime / _totalTime;
        }

        private void ReduceTimeForSelection()
        {
            if (!_IsTimerRunning)
            {
                return;
            }

            _currentTime = Mathf.Max(0f, _currentTime - _penaltyTime);
            _imageFilling.fillAmount = _currentTime / _totalTime;
            CheckTimeIsUp();
        }

        private void CheckTimeIsUp()
        {
            if (_IsTimerRunning && _currentTime <= 0f)
            {
                StopTimer();
                OnTimeIsUp?.Invoke();
            }
        }

        private void OnDestroy()
        {
            _arrow.OnHardZoneSelected -= IncreaseTimeForSelection;
            _arrow.OnWhiteZoneSelected -= ReduceTimeForSelection;
        }
    }
}
