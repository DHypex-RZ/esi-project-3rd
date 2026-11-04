using System;
using UnityEngine;
using static Gameplay.AI.EnemyController;

namespace Gameplay.AI.Behavior.Modules
{
    [Serializable]
    internal sealed class EnemyAttackBehavior : EnemyBehaviorModule
    {
        [SerializeField, Min(1)] int damage;
        [SerializeField, Min(0.1f)] float cooldown;

        float _cooldownRemaining;
        bool _isCooldownStarted;

        public void Tick()
        {
            if (player) return;

            if (!_isCooldownStarted)
            {
                _cooldownRemaining = cooldown;
                _isCooldownStarted = true;
            }

            _cooldownRemaining -= Time.fixedDeltaTime;
            if (_cooldownRemaining > 0f) return;

            player.TakeDamage(damage);
            _cooldownRemaining = cooldown;
        }

        public void Reset()
        {
            _cooldownRemaining = 0f;
            _isCooldownStarted = false;
        }
    }
}
