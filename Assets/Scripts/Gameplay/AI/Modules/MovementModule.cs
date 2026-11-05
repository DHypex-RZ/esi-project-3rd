using UnityEngine;
using static Gameplay.Player.PlayerController;

namespace Gameplay.AI.Modules
{
    [RequireComponent(typeof(EnemyController))]
    public class MovementModule: ModuleController
    {
        [SerializeField] internal float speed;
        [SerializeField] internal float stopDistance;
        [SerializeField] LayerMask layer;
        
        void Start()
        {
            if (transform.position.x < player.position.x) stopDistance = -stopDistance;
        }
        
        void FixedUpdate()
        {
            Vector2 target;
            
            RaycastHit2D hit = Physics2D.Raycast(transform.position, player.position - transform.position, layer);
            
            if (hit.collider) target = hit.point + new Vector2(stopDistance, 0); 
            else target = player.position + new Vector3(stopDistance, 0);
            
            controller.transform.position = Vector2.MoveTowards(controller.transform.position, target, speed * Time.fixedDeltaTime);
        }
    }
}