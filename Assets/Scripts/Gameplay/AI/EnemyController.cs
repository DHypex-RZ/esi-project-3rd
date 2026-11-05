using System;
using Gameplay.AI.Behavior;
using Gameplay.AI.Behavior.Modules;
using UnityEngine;

namespace Gameplay.AI
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class EnemyController : MonoBehaviour
    {

        [Header("Behaviors")]
        [SerializeField] internal EnemyAttackBehavior attack = new();
        [SerializeField] internal EnemyMovementBehavior movement = new();
        [SerializeField] internal EnemySlideBehavior slide = new();

        internal Rigidbody2D rb { get; private set;}
        internal Collider2D col { get; private set;}
        internal bool hit { get; private set;}

        EnemyBehaviorModule[] _modules;
        
        void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            col = GetComponent<Collider2D>();
            
            _modules = new EnemyBehaviorModule[] { attack, movement, slide };
            
            foreach (EnemyBehaviorModule module in _modules) module.Awake(this);
        }

        void Update() 
        {
            foreach (EnemyBehaviorModule module in _modules) module.Update();

            if (!hit) return;
            
            if (hit & slide.enabled)
            {
                hit = false;
                return;
            }
            
            Destroy(gameObject);
        }

        void FixedUpdate()
        {
            foreach (EnemyBehaviorModule module in _modules) module.FixedUpdate();
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("PlayerAttack")) hit = true;
        }
    }
}
