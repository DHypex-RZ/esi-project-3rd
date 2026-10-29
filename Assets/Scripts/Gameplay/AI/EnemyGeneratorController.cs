using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Gameplay.AI
{
    public class EnemyGeneratorController : MonoBehaviour
    {
        [Header("Spawn settings")]
        [SerializeField] Transform spawnPointLeft;
        [SerializeField] Transform spawnPointRight;
        [SerializeField] Transform enemyContainer;
        
        [Header("Player")]
        [SerializeField] Transform player;
        
        public static Vector3 playerPosition { get; private set; }

        [Header("Enemy movement")]
        [SerializeField, Min(0.1f)] float moveSpeed = 5f;
        [SerializeField, Min(0f)] float stopDistance = 1.85f;
        [SerializeField, Min(0f)] float queueSpacing = 4f;

        public static float enemyMoveSpeed { get; private set; }
        public static float enemyStopDistance { get; private set; }
        public static float enemySpacing { get; private set; }

        void Awake()
        {
            enemyMoveSpeed = moveSpeed;
            enemyStopDistance = stopDistance;
            enemySpacing = queueSpacing;
        }

        void Start() { playerPosition = player.position; }

        public void SpawnEnemy(GameObject enemyToGenerate)
        {
            Transform spawnPoint = Random.value < 0.5f ? spawnPointLeft : spawnPointRight;
            Instantiate(enemyToGenerate, spawnPoint.position, Quaternion.identity, enemyContainer);
        }
    }
    
    [Serializable] public struct EnemySpawn
    {
        public GameObject enemy;
        [Min(0)] public int count;
    }
}
