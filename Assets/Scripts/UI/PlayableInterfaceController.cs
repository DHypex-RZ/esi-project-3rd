using Gameplay.Player;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PlayableInterfaceController : MonoBehaviour
    {
        static PlayableInterfaceController _instance;
        
        [SerializeField] PlayerController player;
        [SerializeField] Text scoreText;
        [SerializeField] Image healthFill;

        int _score;

        void Awake()
        {
            _instance = this;
        }

        public static void AddScore(int amount)
        {
            _instance._score += amount;
            _instance.UpdateScoreDisplay();
        }

        public static void UpdateHealthFill(float fillAmount)
        {
            _instance.healthFill.fillAmount = fillAmount;
        }

        void UpdateScoreDisplay()
        {
            scoreText.text = _score.ToString();
        }

        public void PauseMenu()
        {
            Debug.Log("Pause Menu");
        }

        public void OnAttackButton(bool right)
        {
            player.Attack(right);
        }
    }
}
