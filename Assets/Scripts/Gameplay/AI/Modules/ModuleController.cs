using System;
using UnityEngine;

namespace Gameplay.AI.Modules
{
    [Serializable, RequireComponent(typeof(EnemyController))]
    public abstract class ModuleController: MonoBehaviour
    {
        protected internal EnemyController controller;
        
        protected virtual void Awake() => controller = GetComponent<EnemyController>();
    }
}