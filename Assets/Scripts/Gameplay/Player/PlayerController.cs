using System.Collections;
using UI;
using UnityEngine;

namespace Gameplay.Player
{
    public class PlayerController : MonoBehaviour
    {
        public static PlayerController player { get; private set; }
        public static Vector2 playerPosition => player.transform.position;

        [Header("Attack areas")]
        [SerializeField] GameObject attackAreaLeft;
        [SerializeField] GameObject attackAreaRight;
        [SerializeField, Min(0.01f)] float attackDuration;

        [Header("Health")]
        [SerializeField, Min(1)] int maxHealth = 100;

        Coroutine _attackRoutineLeft;
        Coroutine _attackRoutineRight;
        int _currentHealth;

        void Awake()
        {
            player = this;
            
            SetAttackAreaActive(false, false);
            SetAttackAreaActive(true, false);
            maxHealth = Mathf.Max(1, maxHealth);
            _currentHealth = maxHealth;
        }

        void Start()
        {
            UpdateHealthDisplay();
        }

        public void TakeDamage(int damage)
        {
            /*if (damage <= 0 || _currentHealth <= 0) return;

            _currentHealth = Mathf.Max(0, _currentHealth - damage);
            UpdateHealthDisplay();*/
        }

        void UpdateHealthDisplay()
        {
            PlayableInterfaceController.UpdateHealthFill(_currentHealth / maxHealth);
        }

        public void Attack(bool right)
        {
            GameObject attackArea = GetAttackArea(right);
            if (attackArea.activeSelf) return;

            Coroutine attackRoutine = StartCoroutine(AttackRoutine(attackArea));
            SetAttackRoutine(right, attackRoutine);
        }

        IEnumerator AttackRoutine(GameObject attackArea)
        {
            attackArea.SetActive(true);
            yield return new WaitForSeconds(attackDuration);
            attackArea.SetActive(false);
        }

        void StopAttack(bool right)
        {
            Coroutine attackRoutine = right ? _attackRoutineRight : _attackRoutineLeft;
            if (attackRoutine != null) StopCoroutine(attackRoutine);

            SetAttackRoutine(right, null);
            SetAttackAreaActive(right, false);
        }

        GameObject GetAttackArea(bool right) => right ? attackAreaRight : attackAreaLeft;

        void SetAttackAreaActive(bool right, bool active)
        {
            GetAttackArea(right).SetActive(active);
        }

        void SetAttackRoutine(bool right, Coroutine routine)
        {
            if (right) _attackRoutineRight = routine;
            else _attackRoutineLeft = routine;
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Enemy")) return;

            bool right = other.transform.position.x > transform.position.x;
            StopAttack(right);
        }
    }
}
