using System.Collections;
using UnityEngine;

namespace Gameplay.AI
{
    public class EnemyGeneratorController : MonoBehaviour
    {
        [Header("Enemy config")]
        [SerializeField] GameObject enemy;
        
        [Header("Spawn settings")]
        [SerializeField] Transform spawnPointLeft;
        [SerializeField] Transform spawnPointRight;
        [SerializeField, Min(0.1f)] float minSpawnInterval = 1f;
        [SerializeField, Min(0.1f)] float maxSpawnInterval = 2.5f;
        [SerializeField] Transform enemyContainer;
        
        [Header("Player")]
        [SerializeField] Transform player;
        
        public static Vector3 PlayerPosition { get; private set; }
        
        void Awake()
        {
            PlayerPosition = player.position;
        }

        void Start()
        {
            StartCoroutine(SpawnLoop());
        }

        void Update()
        {
            PlayerPosition = player.position;
        }

        IEnumerator SpawnLoop()
        {
            while (true)
            {
                SpawnRandomEnemy();
                float interval = Random.Range(minSpawnInterval, Mathf.Max(minSpawnInterval, maxSpawnInterval));
                yield return new WaitForSeconds(interval);
            }
        }

        void SpawnRandomEnemy()
        {
            bool fromLeft = Random.value < 0.5f;
            Transform spawnPoint = fromLeft ? spawnPointLeft : spawnPointRight;

            Instantiate(enemy, spawnPoint.position, Quaternion.identity, enemyContainer);
        }
    }
}
