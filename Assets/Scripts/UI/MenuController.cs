using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
	public class MenuController : MonoBehaviour
	{
		public static MenuController Menu { get; private set; }

		[SerializeField] GameObject pauseMenu;
		[SerializeField] GameObject retryMenu;
		[SerializeField] GameObject winMenu;

		void Awake() => Menu = this;

		public void PauseMenu(bool pause) => SetMenu(pauseMenu, pause, pause);
		public void RetryMenu() => SetMenu(retryMenu);
		public void WinMenu() => SetMenu(winMenu);

		void SetMenu(GameObject menu, bool active = true, bool pause = false)
		{
			Time.timeScale = pause ? 0 : 1;
			menu.SetActive(active);
		}

		public async void Retry()
		{
			await Awaitable.WaitForSecondsAsync(0.75f);
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}
		public void Quit() => Application.Quit();
	}
}
