using ProjectSchool2026.Challenge.Behaviour;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectSchool2026.Challenge.Circle
{
    public class CircleZone : BootBehaviour
    {
        [SerializeField] private Image _defaultZoneImage;
        [SerializeField] private Image _hardZoneImage;
        
        [SerializeField, Range(0f, 360f)] private float _defaultZoneWidth;
        [SerializeField, Range(0f, 360f)] private float _hardZoneWidth;

        // Все углы считаем как на часах: 0 = верх (12 часов), растут по часовой стрелке
        private readonly float _defaultZoneCenter = 90f;
        private readonly float _hardZoneCenter = 270f;

        public float DefaultZoneCenter => _defaultZoneCenter;
        public float HardZoneCenter => _hardZoneCenter;

        public float DefaultZoneWidth => _defaultZoneWidth;
        public float HardZoneWidth => _hardZoneWidth;

        public override void Boot()
        {
            SetupZone(_defaultZoneImage, _defaultZoneCenter, _defaultZoneWidth);
            SetupZone(_hardZoneImage, _hardZoneCenter, _hardZoneWidth);
        }

        private void OnValidate()
        {
            SetupZone(_defaultZoneImage, _defaultZoneCenter, _defaultZoneWidth);
            SetupZone(_hardZoneImage, _hardZoneCenter, _hardZoneWidth);
        }

        private void SetupZone(Image zoneImage, float center, float width)
        {
            // Заливка начинается сверху и идёт по часовой стрелке — как наши углы
            zoneImage.fillMethod = Image.FillMethod.Radial360;
            zoneImage.fillOrigin = (int)Image.Origin360.Top;
            zoneImage.fillClockwise = true;
            zoneImage.fillAmount = width / 360f;

            // Поворачиваем картинку так, чтобы заливка началась на (center - width / 2)
            // Минус, потому что в Unity положительный угол Z — это поворот против часовой стрелки
            float startAngle = center - width / 2f;
            zoneImage.rectTransform.localEulerAngles = new Vector3(0f, 0f, -startAngle);
        }
    }
}