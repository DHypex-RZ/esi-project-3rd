using System.Collections;
using UnityEngine;

namespace Gameplay.AI
{
    public class EnemyController : MonoBehaviour
    {
        [Header("Enemy prefabs")]
        [SerializeField] GameObject groundEnemies;
        [SerializeField] GameObject airEnemies;

        [Header("Spawn points")]
        [SerializeField] Transform spawnPointForGround;
        [SerializeField] Transform spawnPointForAir;

        [Header("Spawn timing")]
        [SerializeField, Min(0.1f)] float minSpawnInterval = 1f;
        [SerializeField, Min(0.1f)] float maxSpawnInterval = 2.5f;
        [SerializeField, Min(0.1f)] float initialVelocity = 5f;

        readonly GameObject[] _enemies = new GameObject[3];

        void Start()
        {
            _enemies[0] = groundEnemies;
            _enemies[1] = airEnemies;

            StartCoroutine(SpawnLoop());
        }

        IEnumerator SpawnLoop()
        {
            while (true) // ToDo: Add timer to finish the iteration 
            {
                SpawnRandomEnemy();
                float interval = Random.Range(minSpawnInterval, Mathf.Max(minSpawnInterval, maxSpawnInterval));
                yield return new WaitForSeconds(interval);
            }
        }
        
        void SpawnRandomEnemy()
        {
            GameObject selected = _enemies[Random.Range(0, _enemies.Length)];
            Transform spawn = selected == airEnemies ? spawnPointForAir : spawnPointForGround;
            GameObject ins = Instantiate(selected, spawn.position, Quaternion.identity);
            
            if (ins.TryGetComponent(out Rigidbody2D rb)){ rb.AddForceX(-initialVelocity, ForceMode2D.Impulse); }
        }
    }
}

