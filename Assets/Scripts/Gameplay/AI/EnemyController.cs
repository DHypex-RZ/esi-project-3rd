using UnityEngine;
using static Gameplay.AI.EnemyGeneratorController;

namespace Gameplay.AI
{
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class EnemyController : MonoBehaviour
    {
        [Header("Score")]
        [SerializeField, Min(0)] protected int score;

        protected Rigidbody2D rb;

        void Awake() => rb = GetComponent<Rigidbody2D>();

        protected virtual void FixedUpdate()
        {
            float distanceToPlayer = playerPosition.x - rb.position.x;

            if (Mathf.Abs(distanceToPlayer) <= enemyStopDistance || IsBlockedByEnemyAhead())
            {
                rb.linearVelocityX = 0;
                return;
            }

            rb.linearVelocityX = Mathf.Sign(distanceToPlayer) * enemySpeed;
        }

        bool IsBlockedByEnemyAhead()
        {
            RaycastHit2D hit = Physics2D.Raycast(rb.position, transform.right, enemySpacing, enemyLayer);
            return hit && hit.distance < enemySpacing;
        }

        void OnTriggerEnter2D(Collider2D other) { if (other.CompareTag("PlayerAttack")) TakeDamage(); }

        protected abstract void TakeDamage();
    }
}