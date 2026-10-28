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
            StartCoroutine(AttackRoutine(attackArea));
        }

        IEnumerator AttackRoutine(GameObject attackArea)
        {
            attackArea.SetActive(true);
            yield return new WaitForSeconds(attackDuration);
            attackArea.SetActive(false);
        }
    }
}
