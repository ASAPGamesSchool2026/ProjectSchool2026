using UnityEngine;
using UnityEngine.SceneManagement;

namespace ProjectSchool2026.UI.MainMenu
{
	public class MainMenuView : MonoBehaviour
	{
		[SerializeField] private string _gameSceneName = "TileMap";

		public void StartGame()
		{
			SceneManager.LoadScene(_gameSceneName);
		}

		public void ExitGame()
		{
			Application.Quit();
		}
	}
}
