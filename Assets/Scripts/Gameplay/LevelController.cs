using System;
using System.Collections;
using Gameplay.AI;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Gameplay
{
    public class LevelController : MonoBehaviour
    {
        [Header("Enemies")]
        [SerializeField] EnemyGeneratorController enemyGenerator;
        [SerializeField] EnemySpawn[] enemies;

        [Header("Spawn timing")]
        [SerializeField, Min(0.1f)] float minSpawnInterval;
        [SerializeField, Min(0.1f)] float maxSpawnInterval;
        [SerializeField, Min(0.1f)] float spawnRetryInterval;

        int[] _remaining;
        int _totalRemaining;

        void Start()
        {
            InitializeEnemyCounts();
            StartCoroutine(SpawnLoop());
        }

        void InitializeEnemyCounts()
        {
            _remaining = new int[enemies.Length];
            for (int i = 0; i < enemies.Length; i++)
            {
                _remaining[i] = enemies[i].count;
                _totalRemaining += enemies[i].count;
            }
        }

        IEnumerator SpawnLoop()
        {
            while (_totalRemaining > 0)
            {
                int enemyIndex = SelectRandomEnemyIndex();
                while (!enemyGenerator.TrySpawnAnEnemy(enemies[enemyIndex].enemy))
                {
                    yield return new WaitForSeconds(spawnRetryInterval);
                }

                ConsumeEnemy(enemyIndex);

                if (_totalRemaining == 0) yield break;
                yield return new WaitForSeconds(GetRandomSpawnInterval());
            }
        }

        void ConsumeEnemy(int enemyIndex)
        {
            _remaining[enemyIndex]--;
            _totalRemaining--;
        }

        float GetRandomSpawnInterval()
        {
            return Random.Range(minSpawnInterval, Mathf.Max(minSpawnInterval, maxSpawnInterval));
        }

        int SelectRandomEnemyIndex()
        {
            int pick = Random.Range(0, _totalRemaining);

            for (int i = 0; i < _remaining.Length; i++)
            {
                if (pick < _remaining[i]) return i;

                pick -= _remaining[i];
            }

            return -1;
        }
    }
}
