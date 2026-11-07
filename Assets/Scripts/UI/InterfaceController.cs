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
        [SerializeField] Image healthSprite;

        bool _paused;

        void Awake() => canvas = this;

        internal void UpdateScore(int score) => scoreText.text = score.ToString();
        internal void UpdateHealth(float health, float currentHealth) => healthSprite.fillAmount = currentHealth / health;
        internal void RetryMenu() => retryMenu.SetActive(true);
        internal void WinMenu() => winMenu.SetActive(true);

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
