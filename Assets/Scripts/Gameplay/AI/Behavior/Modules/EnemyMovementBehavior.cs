using System;
using UnityEngine;
using static Gameplay.AI.EnemyController;


namespace Gameplay.AI.Behavior.Modules
{
    [Serializable]
    internal sealed class EnemyMovementBehavior : EnemyBehaviorModule
    {
        const float _SEPARATION_SKIN = 0.01f;

        [SerializeField, Min(0.1f)] float speed = 5f;
        [SerializeField, Min(0f)] float stopDistance = 1.85f;
        [SerializeField, Min(0f)] float enemySpacing = 2f;

        [NonSerialized] EnemyCollisionBehavior _collision;
        [NonSerialized] EnemyAttackBehavior _attack;

        protected override void OnInitialize()
        {
            _collision = controller.collisions;
            _attack = controller.attack;
        }

        public void Tick(bool movementIsOverridden)
        {
            if (!movementIsOverridden) MoveTowardsPlayer();
            PreventOverlap();
        }

        public EnemyController MoveSafely(Vector2 targetPosition)
        {
            Vector2 currentPosition = controller.rb.position;
            Vector2 displacement = targetPosition - currentPosition;
            float distance = Mathf.Abs(displacement.x);

            if (distance <= Mathf.Epsilon)
            {
                controller.rb.MovePosition(targetPosition);
                return null;
            }

            Vector2 direction = Vector2.right * Mathf.Sign(displacement.x);
            Vector2 safePosition = targetPosition;
            EnemyController blockingEnemy = null;

            if (_collision.TryFindNearestEnemy(
                    direction,
                    distance + _SEPARATION_SKIN,
                    out EnemyController nearestEnemy,
                    out float hitDistance))
            {
                float safeDistance = Mathf.Min(distance, Mathf.Max(0f, hitDistance - _SEPARATION_SKIN));
                safePosition.x = currentPosition.x + direction.x * safeDistance;
                if (safeDistance < distance) blockingEnemy = nearestEnemy;
            }

            controller.rb.MovePosition(safePosition);
            return blockingEnemy;
        }

        void MoveTowardsPlayer()
        {
            if (player is null)
            {
                controller.rb.linearVelocityX = 0f;
                return;
            }

            float distanceToPlayer = playerPosition.x - controller.rb.position.x;
            float directionToPlayer = Mathf.Sign(distanceToPlayer);

            if (Mathf.Abs(distanceToPlayer) <= stopDistance)
            {
                controller.rb.linearVelocityX = 0f;
                if (!_collision.IsPathBlocked(Vector2.right * directionToPlayer, enemySpacing))
                {
                    _attack.Tick();
                }

                return;
            }

            _attack.Reset();

            if (_collision.IsPathBlocked(Vector2.right * directionToPlayer, enemySpacing))
            {
                controller.rb.linearVelocityX = 0f;
                return;
            }

            controller.rb.linearVelocityX = directionToPlayer * speed;
        }

        void PreventOverlap()
        {
            float velocityX = controller.rb.linearVelocityX;
            if (Mathf.Approximately(velocityX, 0f)) return;

            Vector2 direction = Vector2.right * Mathf.Sign(velocityX);
            float stepDistance = Mathf.Abs(velocityX) * Time.fixedDeltaTime;

            if (!_collision.TryFindNearestEnemy(
                    direction,
                    stepDistance + _SEPARATION_SKIN,
                    out _,
                    out float hitDistance))
            {
                return;
            }

            float safeDistance = Mathf.Max(0f, hitDistance - _SEPARATION_SKIN);
            controller.rb.linearVelocityX = Mathf.Sign(velocityX) * safeDistance / Time.fixedDeltaTime;
        }
    }
}
