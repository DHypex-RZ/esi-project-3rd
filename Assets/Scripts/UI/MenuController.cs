using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
	public class MenuController : MonoBehaviour
	{
		public static MenuController hud { get; private set; }

		const float SECURE_TIME = 0.75f;

		[SerializeField] GameObject pauseMenu;
		[SerializeField] GameObject retryMenu;
		[SerializeField] GameObject winMenu;

		bool _paused;

		void Awake() => hud = this;

		public void RetryMenu() => retryMenu.SetActive(true);
		public void WinMenu() => winMenu.SetActive(true);

		public void Pause(bool pause)
		{
			Time.timeScale = pause ? 0 : 1;
			pauseMenu.SetActive(pause);
		}

		public async void Retry()
		{
			await Awaitable.WaitForSecondsAsync(SECURE_TIME);
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}

		public void Quit() => Application.Quit();
	}
}
