using System;
using UnityEngine;
using static Gameplay.Player.PlayerController;

namespace Gameplay.AI.Behavior.Modules
{
    [Serializable]
    internal sealed class EnemyAttackBehavior : EnemyBehaviorModule
    {
        [SerializeField, Min(0)] int damage;
        [SerializeField, Min(0.1f)] float cooldown;
        [SerializeField, Min(0f)] float attackRange;

        float _cooldownRemaining;
        bool _isCooldownStarted;

        protected internal override void Update()
        {
            if (!enabled) return;
                
            Tick();
        }

        void Tick()
        {
            if (!player || Mathf.Abs(playerPosition.x - controller.rb.position.x) > attackRange)
            {
                _cooldownRemaining = 0f;
                _isCooldownStarted = false;
                return;
            }

            if (!_isCooldownStarted)
            {
                _cooldownRemaining = cooldown;
                _isCooldownStarted = true;
            }

            _cooldownRemaining -= Time.deltaTime;
            
            if (_cooldownRemaining > 0f) return;

            player.TakeDamage(damage);
            _cooldownRemaining = cooldown;
        }
    }
}
