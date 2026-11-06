using UnityEngine;

namespace ProjectSchool2026.UI.MainMenu
{
	public class OpenLink : MonoBehaviour
	{
		[SerializeField] private string _url;

		public void Open()
		{
			if (!string.IsNullOrWhiteSpace(_url))
			{
				Application.OpenURL(_url);
			}
		}
	}
}
