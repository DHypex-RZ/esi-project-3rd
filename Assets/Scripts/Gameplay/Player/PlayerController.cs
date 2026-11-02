using System.Collections;
using UnityEngine;

namespace Gameplay.Player
{
    public class PlayerController : MonoBehaviour
    {
        [Header("Attack areas")]
        [SerializeField] GameObject attackAreaLeft;
        [SerializeField] GameObject attackAreaRight;
        [SerializeField, Min(0.01f)] float attackDuration;

        Coroutine _attackRoutineLeft;
        Coroutine _attackRoutineRight;

        void Awake()
        {
            SetAttackAreaActive(false, false);
            SetAttackAreaActive(true, false);
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
