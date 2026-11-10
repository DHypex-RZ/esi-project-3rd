using Gameplay.AI.Modules;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UI.InterfaceController;

namespace Gameplay.Player
{
    public class PlayerController : MonoBehaviour
    {
        public static PlayerController player { get; private set; }
        
        [SerializeField, Min(0)] float attackCooldown;
        [SerializeField, Min(0)] float hitboxLive;
        [SerializeField, Min(0)] float failCooldown;
        [SerializeField, Min(0)] float health;

        internal bool impact;
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
            await Awaitable.WaitForSecondsAsync(hitboxLive);
            area.SetActive(false);
        }

        public async void AttackCooldown(Button button)
        {
            EventTrigger trigger = button.GetComponent<EventTrigger>();
            trigger.enabled = button.interactable = false;
            trigger.enabled = false;
            await Awaitable.WaitForSecondsAsync(hitboxLive);
            if(impact) await Awaitable.WaitForSecondsAsync(attackCooldown - hitboxLive);
            else await Awaitable.WaitForSecondsAsync(failCooldown - hitboxLive);
            trigger.enabled = button.interactable = true;
            impact = false;
        }

        public void TakeDamage(int damage)
        {
            _animator.Play("Damaged");
            ScoreModule.count = 0;
            _currentHealth -= damage;
            if (_currentHealth <= 0) _currentHealth = 0;
            canvas.UpdateHealth(health, _currentHealth);
            canvas.UpdateMultiplier(1);
            if (dead) canvas.RetryMenu();
        }
    }
}
