using UnityEngine;
using static Gameplay.Player.PlayerController;


namespace Gameplay.AI.Modules
{
    [RequireComponent(typeof(MovementModule))]
    public class SlidingModule: ModuleController
    {
        float _gap = .5f;
        
        [SerializeField, Min(0)] float speed;
        [SerializeField, Min(0)] float duration;
        
        Vector2 _target;
        bool _sliding;

        void Update()
        {
            if (_sliding) transform.position = Vector2.MoveTowards(transform.position, _target, speed * Time.deltaTime);
        }

        async void Slide()
        {
            if (controller.movement.stopDistance > 0) _gap = -_gap;
            _target = player.position + new Vector3(-controller.movement.stopDistance + _gap, 0);
            controller.score.enabled = controller.attack.enabled = controller.movement.enabled = false;
            controller.movement.stopDistance = -controller.movement.stopDistance;
            _sliding = true;
            await Awaitable.WaitForSecondsAsync(duration);
            _sliding = false;
            controller.score.enabled = controller.attack.enabled = controller.movement.enabled = true;
        }
        
        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("PlayerAttack") && controller.score.lives > 0) Slide();
        }
    }
}