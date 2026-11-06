using System;
using UnityEngine;

namespace Gameplay.AI
{
    [Serializable]
    public struct EnemySpawn
    {
        [SerializeField] internal GameObject enemy;
        [SerializeField, Min(1)] public int count;
    }
}