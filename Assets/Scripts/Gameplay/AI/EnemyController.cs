using Gameplay.AI.Behavior;
using Gameplay.AI.Behavior.Modules;
using Gameplay.Player;
using UI;
using UnityEngine;

namespace Gameplay.AI
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class EnemyController : MonoBehaviour
    {
        internal static PlayerController player;
        internal static Vector2 playerPosition => player.transform.position;
        
        [SerializeField, Min(0)] protected int score;

        [Header("Behaviors")]
        [SerializeField] internal EnemyAttackBehavior attack = new();
        [SerializeField] internal EnemyCollisionBehavior collisions = new();
        [SerializeField] internal EnemyMovementBehavior movement = new();
        [SerializeField] internal EnemyPushBehavior push = new();
        [SerializeField] internal EnemySlideBehavior slide = new();

        internal Rigidbody2D rb;
        
        EnemyBehaviorModule[] _behaviorModules;
        
        
        internal void BeginPushSequence() => push.BeginSequence();
        internal void Push(EnemyController firstEnemy, Vector2 direction) =>
            push.Push(firstEnemy, direction);
        internal void ApplyPush(Vector2 direction, float force, float duration) =>
            push.Apply(direction, force, duration);

        void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            player = FindFirstObjectByType<PlayerController>();
            _behaviorModules = new EnemyBehaviorModule[] { attack, collisions, movement, push, slide};
            
            foreach (EnemyBehaviorModule module in _behaviorModules) module.Initialize(this);
        }

        protected virtual void FixedUpdate()
        {
            bool moduleControlsMovement = TickModules();
            movement.Tick(push.Tick() || moduleControlsMovement);
        }

        public void Push(Vector2 direction)
        {
            push.Push(direction);
        }

        internal EnemyController FindEnemyInDirection(Vector2 direction, float distance)
        {
            return collisions.TryFindNearestEnemy(direction, distance, out EnemyController enemy, out _) ? enemy : null;
        }

        internal EnemyController MoveSafely(Vector2 targetPosition)
        {
            return movement.MoveSafely(targetPosition);
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("PlayerAttack") && !HandleDamage())
            {
                PlayableInterfaceController.AddScore(score);
                Destroy(gameObject);
                return;
            }

            NotifyModulesOfTrigger(other);
        }

        bool HandleDamage()
        {
            foreach (EnemyBehaviorModule module in _behaviorModules)
            {
                if (module.OnEnemyDamaged()) return true;
            }

            return false;
        }

        bool TickModules()
        {
            bool movementIsOverridden = false;
            foreach (EnemyBehaviorModule module in _behaviorModules)
            {
                movementIsOverridden |= module.OnFixedUpdate();
            }

            return movementIsOverridden;
        }

        void NotifyModulesOfTrigger(Collider2D other)
        {
            foreach (EnemyBehaviorModule module in _behaviorModules)
            {
                module.OnTriggerEnter2D(other);
            }
        }
    }
}
