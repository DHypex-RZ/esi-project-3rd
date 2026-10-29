using System;
using System.Collections;
using UnityEngine;

namespace Gameplay.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] float jumpForce;
        [SerializeField] float attackSpeed;

        [Header("Attack areas")]
        [SerializeField] GameObject attackAreaLeft;
        [SerializeField] GameObject attackAreaRight;
        [SerializeField, Min(0.01f)] float attackDuration = 0.2f;

        Rigidbody2D _rb;
        Coroutine _attackRoutineLeft;
        Coroutine _attackRoutineRight;

        void Awake()
        {
            attackAreaLeft.SetActive(false);
            attackAreaRight.SetActive(false);
        }

        void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        public void Attack(bool right)
        {
            GameObject attackArea = right ? attackAreaRight : attackAreaLeft;
            if (attackArea.activeSelf) return;

            if (right) _attackRoutineRight = StartCoroutine(AttackRoutine(attackArea));
            else _attackRoutineLeft = StartCoroutine(AttackRoutine(attackArea));
        }

        IEnumerator AttackRoutine(GameObject attackArea)
        {
            attackArea.SetActive(true);
            yield return new WaitForSeconds(attackDuration);
            attackArea.SetActive(false);
        }

        void StopAttack(bool right)
        {
            Coroutine routine = right ? _attackRoutineRight : _attackRoutineLeft;
            if (routine != null) StopCoroutine(routine);

            if (right)
            {
                _attackRoutineRight = null;
                attackAreaRight.SetActive(false);
            }
            else
            {
                _attackRoutineLeft = null;
                attackAreaLeft.SetActive(false);
            }
        }
        
        void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Enemy")) return;

            bool right = other.transform.position.x > transform.position.x;
            StopAttack(right);
        }
    }
}
