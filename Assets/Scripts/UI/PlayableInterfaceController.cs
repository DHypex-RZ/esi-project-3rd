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

        int _score;

        void Start()
        {
            _instance = this;
        }

        public static void AddScore(int amount)
        {
            _instance._score += amount;
            _instance.UpdateScoreDisplay();
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
