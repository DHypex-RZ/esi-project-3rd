using System.Collections.Generic;
using UnityEngine;
using static Gameplay.AI.EnemyGeneratorController;

namespace Gameplay.AI
{
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class EnemyController : MonoBehaviour
    {
        [Header("Score")]
        [SerializeField, Min(0)] protected int score;

        protected Rigidbody2D rb { get; private set; }

        static readonly List<EnemyController> _ActiveEnemies = new();
        static int _spawnCounter;

        int _spawnOrder;

        void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            _spawnOrder = _spawnCounter++;
        }

        void OnEnable() => _ActiveEnemies.Add(this);

        void OnDisable() => _ActiveEnemies.Remove(this);

        protected virtual void FixedUpdate()
        {
            float distance = playerPosition.x - rb.position.x;

            if (Mathf.Abs(distance) <= enemyStopDistance || IsBlockedByEnemyAhead(distance))
            {
                rb.linearVelocityX = 0f;
                return;
            }

            rb.linearVelocityX = Mathf.Sign(distance) * enemyMoveSpeed;
        }
        
        bool IsBlockedByEnemyAhead(float distance)
        {
            float ownDistance = Mathf.Abs(distance);

            foreach (EnemyController other in _ActiveEnemies)
            {
                if (other == this) continue;

                float otherDistance = playerPosition.x - other.rb.position.x;
                if (!Mathf.Approximately(Mathf.Sign(otherDistance), Mathf.Sign(distance))) continue;

                float gap = ownDistance - Mathf.Abs(otherDistance);
                bool isAhead = gap > 0f || (Mathf.Approximately(gap, 0f) && other._spawnOrder < _spawnOrder);

                if (isAhead && gap < enemySpacing) return true;
            }

            return false;
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("PlayerAttack")) TakeDamage();
        }

        protected abstract void TakeDamage();
    }
}