using UnityEngine;
using static UI.InterfaceController;

namespace Gameplay.Player
{
    public class PlayerController : MonoBehaviour
    {
        public static PlayerController player { get; private set; }
        
        [SerializeField, Min(0)] int health;
        
        int _currentHealth;

        void Awake() {
            player = this;
            _currentHealth = health;
        }
        
        public async void Attack(GameObject area)
        {
            area.SetActive(true);
            await Awaitable.WaitForSecondsAsync(0.1f);
            area.SetActive(false);
        }

        internal void TakeDamage(int damage)
        {
            _currentHealth -= damage;
            if (_currentHealth <= 0) _currentHealth = 0;
            canvas.UpdateHealth(health, _currentHealth);
        }
    }
}
