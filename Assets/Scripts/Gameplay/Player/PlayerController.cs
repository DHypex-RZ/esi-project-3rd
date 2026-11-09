using UnityEngine;
using UnityEngine.UI;
using static UI.InterfaceController;

namespace Gameplay.Player
{
    public class PlayerController : MonoBehaviour
    {
        public static PlayerController player { get; private set; }
        
        [SerializeField, Min(0)] float attackCooldown;
        [SerializeField, Min(0)] float health;
        
        internal bool dead => _currentHealth <= 0;

        Animator _animator;
        float _currentHealth;

        void Awake()
        {
            player = this;
            _animator = GetComponent<Animator>();
            _currentHealth = health;
        }
        
        public async void Attack(GameObject area)
        {
            _animator.Play("Attacking");
            area.SetActive(true);
            await Awaitable.WaitForSecondsAsync(0.1f);
            area.SetActive(false);
        }

        public async void AttackCooldown(Button button)
        {
            button.interactable = false;
            await Awaitable.WaitForSecondsAsync(attackCooldown);
            button.interactable = true;
        }

        public void TakeDamage(int damage)
        {
            _currentHealth -= damage;
            if (_currentHealth <= 0) _currentHealth = 0;
            canvas.UpdateHealth(health, _currentHealth);
            if (dead) canvas.RetryMenu();
        }
    }
}
