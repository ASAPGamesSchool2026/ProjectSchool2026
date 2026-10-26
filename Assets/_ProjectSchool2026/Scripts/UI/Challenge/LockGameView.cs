using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectSchool2026.UI.Challenge
{
    public class LockGameView : MonoBehaviour
    {
        [Header("Замок")]
        [SerializeField] private RectTransform _needlePivot;
        [SerializeField] private RectTransform _lockPanel;
        [SerializeField] private Image _yellowZone;
        [SerializeField] private Image _blueZone;
        [SerializeField] private Image _missFlash;

        [Header("Счёт и время")]
        [SerializeField] private Image _timeFill;
        [SerializeField] private TMP_Text _scoreText;
        [SerializeField] private TMP_Text _comboText;

        [Header("Результат")]
        [SerializeField] private GameObject _resultPanel;
        [SerializeField] private TMP_Text _resultTitle;
        [SerializeField] private TMP_Text _resultText;
        [SerializeField] private Button _collectButton;
        [SerializeField] private Button _exitButton;

        public void SetArrow(float angle)
        {
            _needlePivot.localEulerAngles = new Vector3(0f, 0f, -angle);
        }

        public void SetZones(float yellowAngle, float yellowWidth, float blueAngle, float blueWidth)
        {
            SetZone(_yellowZone, yellowAngle, yellowWidth);
            SetZone(_blueZone, blueAngle, blueWidth);
        }

        private void SetZone(Image image, float center, float width)
        {
            image.fillAmount = width / 360f;
            image.rectTransform.localEulerAngles = new Vector3(0f, 0f, -(center - width / 2f));
        }

        public void SetScore(int score, int target, int combo)
        {
            _scoreText.text = $"Очки: {score} / {target}";
            _comboText.text = $"Комбо: {combo}";
        }

        public void SetTime(float fraction)
        {
            _timeFill.fillAmount = Mathf.Clamp01(fraction);
        }

        public void ShowHit()
        {
            _lockPanel.DOKill();
            _lockPanel.localScale = Vector3.one;
            _lockPanel.DOPunchScale(Vector3.one * 0.035f, 0.18f, 2, 0.4f)
                .SetLink(gameObject);
        }

        public void ShowMiss()
        {
            _missFlash.DOKill();
            _missFlash.color = new Color(0.85f, 0.12f, 0.12f, 0.3f);
            _missFlash.DOFade(0f, 0.2f).SetLink(gameObject);
        }

        public void HideResult()
        {
            _resultPanel.SetActive(false);
            _lockPanel.DOKill();
            _lockPanel.localScale = Vector3.one;
            _missFlash.DOKill();
            _missFlash.color = Color.clear;
        }

        public void ShowResult(bool won, bool hasArtifact, int score)
        {
            _resultPanel.SetActive(true);
            _resultTitle.text = won ? "Замок открыт!" : "Время вышло";
            _resultText.text = $"Набрано очков: {score}";

            if (won && !hasArtifact)
            {
                _resultText.text += "\nДля получения артефакта начни испытание из мира.";
            }

            _collectButton.gameObject.SetActive(won && hasArtifact);
            _exitButton.gameObject.SetActive(!won || !hasArtifact);
        }
    }
}
