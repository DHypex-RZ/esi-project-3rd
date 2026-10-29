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

        int score { get; set; }

        void Awake()
        {
            score = 0;
        }
        
        void Start()
        {
            if (_instance == null) _instance = this;
        }

        public static void AddScore(int amount)
        {
            _instance.score += amount;
            _instance.scoreText.text = _instance.score.ToString();
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
