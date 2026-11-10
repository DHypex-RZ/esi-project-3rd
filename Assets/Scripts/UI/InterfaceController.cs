using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
	public class InterfaceController : MonoBehaviour
	{
		public static InterfaceController canvas { get; private set; }

		const float SECURE_TIME = 0.75f;

		[SerializeField] GameObject pauseMenu;
		[SerializeField] GameObject retryMenu;
		[SerializeField] GameObject winMenu;
		[SerializeField] Text scoreText;
		[SerializeField] Text multiplierText;
		[SerializeField] Image healthSprite;

		bool _paused;

		void Awake() => canvas = this;

		public void UpdateScore(int score) => scoreText.text = score.ToString();
		public void UpdateMultiplier(float multiplier) => multiplierText.text = "x " + multiplier;
		public void RetryMenu() => retryMenu.SetActive(true);
		public void WinMenu() => winMenu.SetActive(true);
		public void UpdateHealth(float health, float currentHealth) => healthSprite.fillAmount = currentHealth / health;

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
