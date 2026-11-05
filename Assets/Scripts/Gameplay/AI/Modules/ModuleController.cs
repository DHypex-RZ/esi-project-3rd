using System;
using UnityEngine;

namespace Gameplay.AI.Modules
{
    [Serializable]
    public abstract class ModuleController: MonoBehaviour
    {
        protected internal EnemyController controller;

        protected void Awake()
        {
            controller = GetComponent<EnemyController>();
        }
    }
}