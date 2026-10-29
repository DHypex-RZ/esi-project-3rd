using System;
using System.Collections;
using Gameplay.AI;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Gameplay
{
    public class LevelController: MonoBehaviour
    {
        [Header("Enemies")]
        [SerializeField] EnemyGeneratorController enemyGenerator;
        [SerializeField] EnemySpawn[] enemies;

        [Header("Spawn timing")]
        [SerializeField, Min(0.1f)] float minSpawnInterval = 1f;
        [SerializeField, Min(0.1f)] float maxSpawnInterval = 2.5f;

        int[] _remaining;
        int _totalRemaining;

        void Start()
        {
            _remaining = new int[enemies.Length];
            for (int i = 0; i < enemies.Length; i++)
            {
                _remaining[i] = enemies[i].count;
                _totalRemaining += enemies[i].count;
            }

            StartCoroutine(SpawnLoop());
        }

        IEnumerator SpawnLoop()
        {
            while (_totalRemaining > 0)
            {
                enemyGenerator.SpawnEnemy(PickRandomEnemy());
                float interval = Random.Range(minSpawnInterval, Mathf.Max(minSpawnInterval, maxSpawnInterval));
                yield return new WaitForSeconds(interval);
            }
        }

        GameObject PickRandomEnemy()
        {
            int pick = Random.Range(0, _totalRemaining);

            for (int i = 0; i < _remaining.Length; i++)
            {
                if (pick < _remaining[i])
                {
                    _remaining[i]--;
                    _totalRemaining--;
                    return enemies[i].enemy;
                }

                pick -= _remaining[i];
            }

            return null;
        }
    }
}
