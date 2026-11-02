using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Gameplay.AI
{
    public class EnemyGeneratorController : MonoBehaviour
    {
        [Header("Player")]
        [SerializeField] Transform player;
        
        [Header("Spawn settings")]
        [SerializeField] Transform spawnPointLeft;
        [SerializeField] Transform spawnPointRight;
        [SerializeField] Transform enemyContainer;
        [SerializeField, Min(0.01f)] float spawnClearanceRadius = 1.25f;
        
        [Header("Global enemy settings")]
        [SerializeField, Min(0.1f)] float speed;
        [SerializeField, Min(0f)] float stopDistance;
        [SerializeField, Min(0f)] float queueSpacing;
        [SerializeField] LayerMask layer;

        public static Vector3 playerPosition { get; private set; }
        public static float enemySpeed { get; private set; }
        public static float enemyStopDistance { get; private set; }
        public static float enemySpacing { get; private set; }
        public static LayerMask enemyLayer { get; private set; }

        void Awake()
        {
            enemySpeed = speed;
            enemyStopDistance = stopDistance;
            enemySpacing = queueSpacing;
            enemyLayer = layer;
        }

        void Start()
        {
            playerPosition = player.position;
        }

        public bool TrySpawnAnEnemy(GameObject enemyToGenerate)
        {
            bool tryLeftFirst = Random.value < 0.5f;
            Transform firstSpawn = tryLeftFirst ? spawnPointLeft : spawnPointRight;
            Transform secondSpawn = tryLeftFirst ? spawnPointRight : spawnPointLeft;

            if (TrySpawnAt(enemyToGenerate, firstSpawn)) return true;
            return TrySpawnAt(enemyToGenerate, secondSpawn);
        }

        bool TrySpawnAt(GameObject enemyToGenerate, Transform spawnPoint)
        {
            if (Physics2D.OverlapCircle(spawnPoint.position, spawnClearanceRadius, enemyLayer)) return false;

            Instantiate(enemyToGenerate, spawnPoint.position, Quaternion.identity, enemyContainer);
            return true;
        }
    }

    [Serializable]
    public struct EnemySpawn
    {
        public GameObject enemy;
        [Min(0)] public int count;
    }
}
