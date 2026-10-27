using System;
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
        
        public event Action OnDefaultZoneSelected;
        public event Action OnHardZoneSelected;
        public event Action OnWhiteZoneSelected;
        
        public override void Boot()
        {
            StartRotate();
        }

        public override void ClickHandler()
        {
            TrySelectZone();
        }

        private void StartRotate()
        {
            _isRotating = true; 
        }

        public void StopRotate()
        {
            _isRotating = false;
        }

        private void TrySelectZone()
        {
            bool isArrowInDefaultZone = CheckArrowInsideZone(_circleZone.DefaultZoneCenter,
                _circleZone.DefaultZoneWidth);
            
            bool isArrowInHardZone = CheckArrowInsideZone(_circleZone.HardZoneCenter, _circleZone.HardZoneWidth);

            if (isArrowInDefaultZone)
            {
                Debug.Log("Попал в Default Zone");
                OnDefaultZoneSelected?.Invoke();
                return;
            }

            if (isArrowInHardZone)
            {
                Debug.Log("Попал в Hard Zone");
                OnHardZoneSelected?.Invoke();
                return;
            }
            
            OnWhiteZoneSelected?.Invoke();
        }

        private bool CheckArrowInsideZone(float zoneCenter, float zoneWidth)
        {
            // Угол стрелки как на часах (0 = верх, по часовой стрелке), как и у зон.
            // Минус, потому что в Unity Z идет против часовой стрелки
            float arrowAngle = -transform.localEulerAngles.z;

            // DeltaAngle — кратчайшая разница между углами
            float difference = Mathf.Abs(Mathf.DeltaAngle(arrowAngle, zoneCenter));

            // Стрелка внутри зоны, если отклонилась от центра не больше чем на половину ширины
            return difference <= zoneWidth / 2f;
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