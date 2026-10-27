using ProjectSchool2026.Challenge.Bootstrap;
using UnityEngine;

namespace ProjectSchool2026.Challenge.Arrow
{
    public class Arrow : BootBehaviour
    {
        [SerializeField, Range(-360f, 360f)] private float _speed;

        private bool _isRotating;
        
        public override void Boot(ChallengeBootstrap bootstrap)
        {
            StartRotate();
            bootstrap.OnClick += TrySelectZone;
        }

        private void StartRotate()
        {
            _isRotating = true; 
        }

        private void TrySelectZone()
        {
            
        }

        private void Update()
        {
            if (_isRotating)
            {
                transform.Rotate(Vector3.forward * _speed * Time.deltaTime);
            }
        }
    }
}