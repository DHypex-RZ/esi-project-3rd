using UnityEngine;
using static Gameplay.AI.EnemyGeneratorController;

namespace Gameplay.AI
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class EnemyController : MonoBehaviour
    {
        [Header("Movement")]
        [SerializeField, Min(0.1f)] float moveSpeed = 5f;
        [SerializeField, Min(0f)] float stopDistance = 1.5f;

        Rigidbody2D _rb;

        void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        void FixedUpdate()
        {
            float distance = PlayerPosition.x - _rb.position.x;

            if (Mathf.Abs(distance) <= stopDistance)
            {
                _rb.linearVelocityX = 0f;
                return;
            }

            _rb.linearVelocityX = Mathf.Sign(distance) * moveSpeed;
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("PlayerAttack")) { TakeDamage(); }
        }

        void TakeDamage()
        {
            Destroy(gameObject);
        }
    }
}