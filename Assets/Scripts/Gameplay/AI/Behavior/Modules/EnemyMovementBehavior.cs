using System;
using UnityEngine;
using static Gameplay.Player.PlayerController;

namespace Gameplay.AI.Behavior.Modules
{
    [Serializable]
    internal sealed class EnemyMovementBehavior : EnemyBehaviorModule
    {
        [SerializeField, Min(0.1f)] float speed;
        [SerializeField, Min(0f)] float stopDistance;
        [SerializeField, Min(0f)] float safeDistance = 1.25f;
        [SerializeField, Min(0f)] float releaseDistance = 1f;
        [SerializeField] LayerMask layer;

        protected internal override void FixedUpdate()
        {
            if (!enabled) return;
            

            if (IsEnemyAhead(out RaycastHit2D hit))
            {
                MoveAway(hit.transform);
                return;
            }

            MoveForwards();
        }

        void MoveAway(Transform other)
        {
            float distanceToOther = other.position.x - controller.rb.position.x;
            
            controller.rb.linearVelocityX = Mathf.Abs(distanceToOther) <= releaseDistance ? 0f : -controller.transform.right.x * speed;
        }

        bool IsEnemyAhead(out RaycastHit2D hit)
        {
            hit = Physics2D.Raycast(controller.transform.position, controller.transform.right, safeDistance, layer);
            
            return hit.collider;
        }

        void MoveForwards()
        {
            float distanceToPlayer = playerPosition.x - controller.rb.position.x;

            controller.rb.linearVelocityX = Mathf.Abs(distanceToPlayer) <= stopDistance ? 0f : controller.transform.right.x * speed;
        }
    }
}