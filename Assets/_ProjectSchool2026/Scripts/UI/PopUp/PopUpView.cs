using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace _ProjectSchool2026.UI.PopUp
{
    public class PopUpView : MonoBehaviour
    {
        [Header("Animation")]
        [SerializeField] private float _animationDuration = 0.5f;
        
        private WaitForSeconds _animationDurationWait;

        private void Awake()
        {
        }

        public void EnableView()
        {
        }

        public void DisableView()
        {
        }
        
        private IEnumerator DisableViewRoutine()
        {
            yield break;
        }
    }
}