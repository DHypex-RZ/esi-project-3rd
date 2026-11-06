using UnityEngine;
using static UI.InterfaceController;

namespace Gameplay.AI.Modules
{
    public class ScoreModule: ModuleController
    {
        static int _totalScore;
        
        [SerializeField, Min(0)] internal int hitPoints;
        [SerializeField, Min(0)] int score;
        
        internal bool live => hitPoints > 0;
        
        void BeingAttacked()
        {
            hitPoints--;
            
            if (live) return;
            
            _totalScore += score;
            canvas.UpdateScore(_totalScore);
            Destroy(gameObject);
        }
        
        void OnTriggerEnter2D(Collider2D other) { if (other.CompareTag("PlayerAttack")) BeingAttacked(); }
    }
}