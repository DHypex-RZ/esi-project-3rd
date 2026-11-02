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
            if (!_instance || _instance) _instance = this;
        }

        public static void AddScore(int amount)
        {
            _instance._score += amount;
            _instance.scoreText.text = _instance._score.ToString();
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
