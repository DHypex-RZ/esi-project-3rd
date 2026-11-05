using System;
using UnityEngine;

namespace Gameplay.AI.Behavior
{
    [Serializable]
    public abstract class EnemyBehaviorModule
    {
        [SerializeField] protected internal bool enabled = true;
        
        protected EnemyController controller;

        internal void Awake(EnemyController controller)
        {
            this.controller = controller;
            Awake();
        }

        protected internal virtual void Awake() { }

        protected internal virtual void Update() { }

        protected internal virtual void FixedUpdate() { }

        protected void OnTriggerEnter2D(Collider2D other) { }
    }
}
