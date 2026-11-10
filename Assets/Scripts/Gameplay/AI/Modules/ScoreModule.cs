using UnityEngine;
using static UI.InterfaceController;
using static Gameplay.Player.PlayerController;

namespace Gameplay.AI.Modules
{
    public class ScoreModule: ModuleController
    {
        static int _totalScore;
        static float _multiplier;

        public static int count { private get; set; }

        [SerializeField, Min(0)] internal int hitPoints;
        [SerializeField, Min(0)] int score;

        internal bool live => hitPoints > 0;

        void BeingAttacked()
        {
            hitPoints--;
            player.impact = true;

            if (live) return;

            CalculateScore();
            canvas.UpdateScore(_totalScore);
            Destroy(gameObject);
        }

        void CalculateScore()
        {
            count++;
            _multiplier = 1 + (float) count / 5 * 0.5f;
            _totalScore += Mathf.RoundToInt(score * _multiplier);
            canvas.UpdateMultiplier(_multiplier);
        }

        void OnTriggerEnter2D(Collider2D other) { if (other.CompareTag("PlayerAttack")) BeingAttacked(); }
    }
}