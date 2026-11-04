using TMPro;
using UnityEngine;

namespace ProjectSchool2026
{
	[RequireComponent(typeof(TextMeshProUGUI))]
	public class ArtifactCounterView : MonoBehaviour
	{
		[SerializeField] private TMP_Text _progressText;

		private void Awake()
		{
			if (_progressText == null)
			{
				_progressText = GetComponent<TMP_Text>();
			}
		}

		public void ShowProgress(int collectedArtifactCount, int totalArtifactCount, bool victory)
		{
			if (!victory)
			{
				_progressText.text = $"Артефакты:   {collectedArtifactCount}/{totalArtifactCount}";
			}
			else
			{
				_progressText.text = "Победа! Вы собрали все артефакты.";
			}
		}
	}
}
