using UnityEngine;
using static UI.InterfaceController;

namespace Gameplay.AI.Modules
{
    public class ScoreModule: ModuleController
    {
        static int _totalScore;
        
        [SerializeField, Min(0)] internal int lives;
        [SerializeField] int score;
        
        void BeingAttacked()
        {
            lives--;

            if (lives != 0) return;
            
            _totalScore += score;
            Destroy(gameObject);
        }
        
        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("PlayerAttack")) BeingAttacked();
        }

        void OnDestroy() => canvas.UpdateScore(_totalScore);
    }
}