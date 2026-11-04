using System;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.AI.Behavior.Modules
{
    [Serializable]
    internal sealed class EnemyPushBehavior : EnemyBehaviorModule
    {
        [SerializeField, Min(0.01f)] float pushForce = 8f;
        [SerializeField, Min(0.01f)] float pushDuration = 0.25f;
        [SerializeField, Min(0f)] float enemySpacing = 2f;

        [NonSerialized] HashSet<EnemyController> _pushedEnemies;
        float _remainingDuration;

        protected override void OnInitialize()
        {
            _pushedEnemies = new HashSet<EnemyController>();
        }

        public bool Tick()
        {
            if (_remainingDuration <= 0f) return false;

            _remainingDuration = Mathf.Max(0f, _remainingDuration - Time.fixedDeltaTime);
            return _remainingDuration > 0f;
        }

        public void Push(Vector2 direction)
        {
            BeginSequence();
            Push(controller, direction);
        }

        public void BeginSequence()
        {
            _pushedEnemies.Clear();
        }

        internal void Push(EnemyController firstEnemy, Vector2 direction)
        {
            if (firstEnemy is null || direction.sqrMagnitude <= Mathf.Epsilon ||
                pushForce <= 0f || pushDuration <= 0f)
            {
                return;
            }

            Vector2 pushDirection = direction.normalized;
            EnemyController enemyAhead = firstEnemy;
            while (enemyAhead is not null)
            {
                if (_pushedEnemies.Add(enemyAhead))
                {
                    enemyAhead.ApplyPush(pushDirection, pushForce, pushDuration);
                }

                enemyAhead = enemyAhead.FindEnemyInDirection(pushDirection, enemySpacing);
            }
        }

        public void Apply(Vector2 direction, float force, float duration)
        {
            _remainingDuration = duration;
            controller.rb.linearVelocity += direction * (force / controller.rb.mass);
        }
    }
}
