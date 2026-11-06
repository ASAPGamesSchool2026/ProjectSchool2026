using ProjectSchool2026.Manager;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace ProjectSchool2026.UI.MainMenu
{
	public class EscapeToMenu : MonoBehaviour
	{
		[SerializeField] private string _menuSceneName = "MainMenu";

		private void Update()
		{
			if (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
			{
				if (InteractionManager.Instance != null)
				{
					InteractionManager.Instance.CancelCurrentInteraction();
				}

				SceneManager.LoadScene(_menuSceneName);
			}
		}
	}
}
