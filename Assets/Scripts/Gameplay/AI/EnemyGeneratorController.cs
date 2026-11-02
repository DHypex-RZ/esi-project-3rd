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

        void Start() { playerPosition = player.position; }

        public void SpawnAnEnemy(GameObject enemyToGenerate)
        {
            Transform spawnPoint;
            Quaternion spawnRotation;

            if (Random.value < 0.5f)
            {
                spawnRotation = Quaternion.Euler(0, 0, 0);
                spawnPoint = spawnPointLeft;
            }
            else
            {
                spawnRotation = Quaternion.Euler(0, 180, 0);
                spawnPoint = spawnPointRight;
            }
            
            Instantiate(enemyToGenerate, spawnPoint.position, spawnRotation, enemyContainer);
        }
    }
    
    [Serializable] public struct EnemySpawn
    {
        public GameObject enemy;
        [Min(0)] public int count;
    }
}
