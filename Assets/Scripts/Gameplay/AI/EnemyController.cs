using UnityEngine;
using static Gameplay.AI.EnemyGeneratorController;

namespace Gameplay.AI
{
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class EnemyController : MonoBehaviour
    {
        const int _HIT_CAPACITY = 20;

        [Header("Score")]
        [SerializeField, Min(0)] protected int score;

        protected Rigidbody2D rb;
        float _pushRemaining;
        RaycastHit2D[] _pushHits = new RaycastHit2D[_HIT_CAPACITY];

        void Awake() => rb = GetComponent<Rigidbody2D>();

        protected virtual void FixedUpdate()
        {
            if (IsBeingPushed()) return;

            MoveTowardsPlayer();
        }

        bool IsBeingPushed()
        {
            if (_pushRemaining <= 0f) return false;

            _pushRemaining = Mathf.Max(0f, _pushRemaining - Time.fixedDeltaTime);
            return _pushRemaining > 0f;
        }

        void MoveTowardsPlayer()
        {
            float distanceToPlayer = playerPosition.x - rb.position.x;
            float directionToPlayer = Mathf.Sign(distanceToPlayer);

            if (Mathf.Abs(distanceToPlayer) <= enemyStopDistance || PathIsBlocked(directionToPlayer))
            {
                rb.linearVelocityX = 0f;
                return;
            }

            rb.linearVelocityX = directionToPlayer * enemySpeed;
        }

        public void Push(Vector2 direction, float force, float duration)
        {
            if (direction.sqrMagnitude <= Mathf.Epsilon || force <= 0f || duration <= 0f) return;

            Vector2 pushDirection = direction.normalized;
            ApplyPush(pushDirection, force, duration);

            EnemyController enemyAhead = FindEnemyInDirection(pushDirection);
            while (enemyAhead != null)
            {
                enemyAhead.ApplyPush(pushDirection, force, duration);
                enemyAhead = enemyAhead.FindEnemyInDirection(pushDirection);
            }
        }

        void ApplyPush(Vector2 direction, float force, float duration)
        {
            _pushRemaining = duration;
            rb.AddForce(direction * force, ForceMode2D.Impulse);
        }

        EnemyController FindEnemyInDirection(Vector2 direction)
        {
            if (enemySpacing <= 0f) return null;

            int hitCount = GetPushHits(direction);
            EnemyController nearestEnemy = null;
            float nearestDistance = float.MaxValue;

            for (int i = 0; i < hitCount; i++)
            {
                RaycastHit2D hit = _pushHits[i];
                if (hit.distance >= nearestDistance) continue;

                EnemyController enemy = hit.collider.GetComponentInParent<EnemyController>();
                if (enemy == null || enemy == this ||
                    Vector2.Dot(enemy.rb.position - rb.position, direction) <= 0f) continue;

                nearestEnemy = enemy;
                nearestDistance = hit.distance;
            }

            return nearestEnemy;
        }

        int GetPushHits(Vector2 direction)
        {
            do
            {
                int hitCount = Physics2D.RaycastNonAlloc(rb.position, direction, _pushHits, enemySpacing, enemyLayer);

                if (hitCount < _pushHits.Length) return hitCount;

                System.Array.Resize(ref _pushHits, _pushHits.Length * 2);
            } while (true);
        }

        bool PathIsBlocked(float directionToPlayer)
        {
            if (directionToPlayer == 0f || enemySpacing <= 0f) return false;

            RaycastHit2D hit = Physics2D.Raycast(rb.position, Vector2.right * directionToPlayer, enemySpacing, enemyLayer);
            return hit && hit.distance < enemySpacing;
        }

        protected virtual void OnTriggerEnter2D(Collider2D other) { if (other.CompareTag("PlayerAttack")) TakeDamage(); }

        protected abstract void TakeDamage();
    }
}