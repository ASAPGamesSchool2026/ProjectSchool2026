using ProjectSchool2026.Challenge.Behaviour;
using UnityEngine;

namespace ProjectSchool2026.Challenge.Bootstrap
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private BootBehaviour[] _bootObjects;

        private bool _isInitialized;
        
        private void Awake()
        {
            AppendAllBootObjects();
        }
        
        public void Click()
        {
            if (!_isInitialized)
            {
                foreach (BootBehaviour bootObject in _bootObjects)
                {
                    bootObject.Boot();
                }
                _isInitialized = true;
            }
        }
        
        private void AppendAllBootObjects()
        {
            BootBehaviour[] newBootObjects = FindObjectsByType<BootBehaviour>();

            if (_bootObjects.Length != newBootObjects.Length)
            {
                _bootObjects = newBootObjects;
            }
        }

        private void OnValidate()
        {
            AppendAllBootObjects();
        }
    }
}