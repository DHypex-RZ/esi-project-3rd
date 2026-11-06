using UnityEngine;
using static Gameplay.Player.PlayerController;

namespace Gameplay.AI.Modules
{
    public class AttackModule: ModuleController
    {
        [SerializeField, Min(0)] int damage;
        [SerializeField, Min(0)] float cooldown;
        
        bool _attacking;

        void Update() { if (controller.movement.inFrontOfPlayer && !_attacking) Attack(); }
        
        async void Attack()
        {
            _attacking = true;
            await Awaitable.WaitForSecondsAsync(cooldown);
            
            if(!controller.score.live) return;
            
            controller.animator.Play("Attacking");
            _attacking = false;
        }
        
        void AttackPlayer() => player.TakeDamage(damage);
    }
}