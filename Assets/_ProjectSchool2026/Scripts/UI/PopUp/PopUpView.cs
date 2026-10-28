using System;
using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _ProjectSchool2026.UI.PopUp
{
    public class PopUpView : MonoBehaviour
    {
        [Header("Animation")]
        [SerializeField] private float _animationDuration = 0.5f;

        [Header("Info Artifact")]
        [SerializeField] private TMP_Text _head;
        [SerializeField] private TMP_Text _Info;

        
        private WaitForSeconds _animationDurationWait;

        private void Awake()
        {
            _animationDurationWait = new WaitForSeconds(_animationDuration);
        }

        public void HeadNameView(string artifactName)
        {
            _head.text = artifactName;
        }

        public void InfoView(string artifactInfo)
        {
            _Info.text = artifactInfo;
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