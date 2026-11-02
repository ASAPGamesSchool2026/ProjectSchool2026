using ProjectSchool2026.Challenge.Behaviour;
using ProjectSchool2026.Challenge.Circle;
using UnityEngine;

namespace ProjectSchool2026.Challenge.Arrow
{
    public class Arrow : ClickableBehaviour
    {
        [SerializeField] private CircleZone _circleZone;   
        [SerializeField, Range(-360f, 360f)] private float _speed;

        private bool _isRotating;
        
        public override void Boot()
        {
            StartRotate();
        }

        public override void ClickHandle()
        {
            TrySelectZone();
        }

        private void TrySelectZone()
        {
            bool isDefaultSelected = CheckArrowInZone(_circleZone.DefaultZoneCenter,
                _circleZone.DefaultZoneWidth);

            bool isHardSelected = CheckArrowInZone(_circleZone.HardZoneCenter,
                _circleZone.HardZoneWidth);

            if (isDefaultSelected)
            {
                Debug.Log("Selected zone is default");
                return;
            }

            if (isHardSelected)
            {
                Debug.Log("Selected zone is hard");
                return;
            }
            
            Debug.Log("Selected zone is white");
        }

        private bool CheckArrowInZone(float zoneCenter, float zoneWidth)
        {
            float arrowAngle = -transform.eulerAngles.z;
            
            float difference = Mathf.Abs(Mathf.DeltaAngle(arrowAngle, zoneCenter));
            
            return difference <= zoneWidth / 2;
        }

        private void StartRotate()
        {
            _isRotating = true; 
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