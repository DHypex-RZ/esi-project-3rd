using Gameplay.Player;
using UnityEditor.Animations;
using UnityEngine;
using static Gameplay.Player.PlayerController;

namespace Gameplay.AI.Modules
{
    public class AttackModule: ModuleController
    {
        [SerializeField, Min(0)] int damage;
        [SerializeField, Min(0)] float cooldown;
        
        Animator _animator;
        bool _attacking;
        
        protected override void Awake()
        {
            base.Awake();
            _animator = GetComponent<Animator>();
        }

        void Update()
        {
            if (controller.movement.inFrontOfPlayer && !_attacking) Attack();
        }
        
        async void Attack()
        {
            _attacking = true;
            await Awaitable.WaitForSecondsAsync(cooldown);
            if(!controller.score.live) return;
            _animator.Play("Attacking");
            player.TakeDamage(damage);
            _attacking = false;
            
        }
    }
}