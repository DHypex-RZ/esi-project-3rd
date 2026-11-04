using System;
using UnityEngine;

namespace Gameplay.AI.Behavior
{
    [Serializable]
    public abstract class EnemyBehaviorModule
    {
        [SerializeField] protected bool enabled = true;
        
        protected EnemyController controller;

        internal void Initialize(EnemyController controller)
        {
            this.controller = controller;
            if (enabled) OnInitialize();
        }

        protected virtual void OnInitialize() { }
        internal virtual bool OnFixedUpdate() => false;
        internal virtual bool OnEnemyDamaged() => false;
        internal virtual void OnTriggerEnter2D(Collider2D other) { }
    }
}
