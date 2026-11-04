using System;
using UnityEngine;

namespace Gameplay.AI.Behavior.Modules
{
    [Serializable]
    internal sealed class EnemyCollisionBehavior : EnemyBehaviorModule
    {
        const int _INITIAL_HIT_CAPACITY = 20;

        [SerializeField] LayerMask enemyLayer;

        Collider2D _collider;
        ContactFilter2D _filter;
        RaycastHit2D[] _hits;

        protected override void OnInitialize()
        {
            _collider = controller.GetComponent<Collider2D>();
            _filter = new ContactFilter2D { useTriggers = true };
            _hits = new RaycastHit2D[_INITIAL_HIT_CAPACITY];
        }

        public bool IsPathBlocked(Vector2 direction, float distance)
        {
            return TryFindNearestEnemy(direction, distance, out _, out _);
        }

        public bool TryFindNearestEnemy(Vector2 direction, float distance, out EnemyController nearestEnemy, out float nearestDistance)
        {
            nearestEnemy = null;
            nearestDistance = float.MaxValue;

            if (_collider is null || distance <= 0f) return false;

            int hitCount = GetHits(direction, distance);
            for (int i = 0; i < hitCount; i++)
            {
                RaycastHit2D hit = _hits[i];
                if (hit.distance >= nearestDistance) continue;

                if (!hit.collider || !hit.collider.TryGetComponent(out EnemyController companion)) continue;
                if (!(Vector2.Dot(companion.rb.position - controller.rb.position, direction) > 0f)) continue;
                
                nearestEnemy = companion;
                nearestDistance = hit.distance;
            }

            return nearestEnemy is null;
        }

        int GetHits(Vector2 direction, float distance)
        {
            _filter.SetLayerMask(enemyLayer);

            while (true)
            {
                int hitCount = _collider.Cast(direction, _filter, _hits, distance, true);
                if (hitCount < _hits.Length) return hitCount;

                Array.Resize(ref _hits, _hits.Length * 2);
            }
        }
    }
}
