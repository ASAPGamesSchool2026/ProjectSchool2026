using System;
using ProjectSchool2026.Challenge.Bootstrap;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectSchool2026.Challenge.Timer
{
    public class ChallengeTimer : BootBehavior
    {
        [Header("Timer Settings")]
        [SerializeField] private Image _timerImage;
        [SerializeField, Range(0f, 100f)] private float _initialTime;
        [SerializeField, Range(0f, 100f)] private float _timeStep;
        
        private float _currentTime;
        
        private bool _isTimerRunning;
        
        public override void Boot()
        {
            _currentTime = _initialTime;
            _timerImage.fillAmount = _currentTime / _initialTime;
            _isTimerRunning = true;
        }

        private void Update()
        {
            if (_isTimerRunning)
            {
                _currentTime -= _timeStep * Time.deltaTime;
                _timerImage.fillAmount = _currentTime / _initialTime;

                if (_currentTime <= 0f)
                {
                    _currentTime = 0f;
                    _timerImage.fillAmount = 0f;
                    _isTimerRunning = false;
                }
            }
        }
    }
}