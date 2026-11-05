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
        [SerializeField, Min(0f)] float rayLength;
        [SerializeField] LayerMask layer;

        Transform _position;

        Transform _target;
        
        protected internal override void Awake() { _position = controller.transform; }

        protected internal override void FixedUpdate()
        {
            if (!enabled) return;

            float distanceToTarget;
            bool backwards = IsEnemyAhead(out RaycastHit2D hit);
            
            if(backwards && !_target) _target = hit.transform;
            
            if (_target) distanceToTarget = _target.transform.position.x - controller.rb.position.x;
            else distanceToTarget = playerPosition.x - controller.rb.position.x;
            
            MoveForwards(distanceToTarget, backwards );
        }

        bool IsEnemyAhead(out RaycastHit2D hit)
        {
            hit = Physics2D.Raycast(_position.position, _position.right, rayLength, layer);
            Debug.DrawRay(_position.position, _position.right * rayLength, Color.red);

            return hit.collider;
        }

        void MoveForwards(float distance, bool backwards)
        {
            if (_target) controller.rb.linearVelocityX = Mathf.Abs(distance) >= stopDistance ? 0f : _position.right.x * -speed;
            
            if (backwards) controller.rb.linearVelocityX = _position.right.x * -speed;
            
            controller.rb.linearVelocityX = Mathf.Abs(distance) <= stopDistance ? 0f : _position.right.x * speed;
        }
    }
}