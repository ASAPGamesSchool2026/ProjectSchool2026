using DG.Tweening;
using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace _ProjectSchool2026.UI.PopUp
{
    public class PopUpView : MonoBehaviour
    {
        [Header("Animation")]
        [SerializeField] private float _animationDuration = 0.5f;
        [SerializeField] private TMP_Text _titleText;
        [SerializeField] private TMP_Text _descriptionText;

        private WaitForSeconds _animationDurationWait;

        private void Awake()
        {
            _animationDurationWait = new WaitForSeconds(_animationDuration);
        }

        public void SetTitle(string title)
        {
            if (_titleText != null)
            {
                _titleText.text = title;
            }
        }
        public void SetDescription(string description)
        {
            if (_descriptionText != null)
            {
                _descriptionText.text = description;
            }
        }

        public void EnableView()
        {
            gameObject.SetActive(true);
            transform.localScale = Vector3.zero;
            transform.DOScale(Vector3.one, _animationDuration);
        }

        public void DisableView()
        {
            StartCoroutine(DisableViewRoutine());
        }
        
        private IEnumerator DisableViewRoutine()
        {
            transform.DOScale(Vector3.zero, _animationDuration);
            yield return _animationDurationWait;
            gameObject.SetActive(false);
        }
    }
}