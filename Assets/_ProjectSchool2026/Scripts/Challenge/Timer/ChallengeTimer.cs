using ProjectSchool2026.Challenge.Bootstrap;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectSchool2026.Challenge.Timer
{
    public class ChallengeTimer : MonoBehaviour
    {
        [SerializeField] private ChallengeBootstrap _bootstrap;
        
        [SerializeField] private Image _imageFilling;

        [Header("Timer Settings")] 
        [SerializeField, Range(10f, 100f)] private float _totalTime;
        [SerializeField, Range(0.001f, 100f)] private float _timeStep;
        
        private float _currentTime;

        private bool _timerRunning;

        private void Awake()
        {
            _currentTime = _totalTime;
            _bootstrap.OnChallengeStarted += StartTimer;
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
    }
}