using UnityEngine;

namespace ProjectSchool2026.Challenge.Bootstrap
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private BootBehavior[] _bootObject;

        private bool _isInitialized;
        
        public void Click()
        {
            if (!_isInitialized)
            {
                foreach (BootBehavior bootObject in _bootObject)
                {
                    bootObject.Boot();
                }
                _isInitialized = true;
            }
        }
    }
}