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

        private readonly float _defaultZoneCenter = 270f;
        private readonly float _hardZoneCenter = 120f;

        public float DefaultZoneWidth => _defaultZoneWidth;
        public float HardZoneWidth => _hardZoneWidth;
        public float DefaultZoneCenter => _defaultZoneCenter;
        public float HardZoneCenter => _hardZoneCenter;
        
        public override void Boot()
        {
            SetupZone(_defaultZoneImage, _defaultZoneWidth, _defaultZoneCenter);
            SetupZone(_hardZoneImage, _hardZoneWidth, _hardZoneCenter);
        }

        private void OnValidate()
        {
            SetupZone(_defaultZoneImage, _defaultZoneWidth, _defaultZoneCenter);
            SetupZone(_hardZoneImage, _hardZoneWidth, _hardZoneCenter);
        }

        private void SetupZone(Image zoneImage, float zoneWidth, float zoneCenter)
        {
            zoneImage.fillAmount = zoneWidth / 360f;
            float startAngel = zoneCenter - zoneWidth / 2f;
            zoneImage.rectTransform.localEulerAngles = new Vector3(0f, 0f, -startAngel);
        }
    }
}