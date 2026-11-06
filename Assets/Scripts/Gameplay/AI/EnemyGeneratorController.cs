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
        [SerializeField, Min(0.01f)] float spawnClearanceRadius = 1.25f;

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
            Collider2D[] overlappingColliders = Physics2D.OverlapCircleAll(spawnPoint.position, spawnClearanceRadius);
            foreach (Collider2D overlappingCollider in overlappingColliders)
            {
                if (overlappingCollider.GetComponentInParent<EnemyController>() != null) return false;
            }
            
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