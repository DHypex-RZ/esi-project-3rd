using System.Collections.Generic;
using UnityEngine;
using static Gameplay.AI.EnemyController;
using Random = UnityEngine.Random;

namespace Gameplay
{
    public class LevelController : MonoBehaviour
    {
        [SerializeField] Transform leftSpawn;
        [SerializeField] Transform rightSpawn;
        [SerializeField] Transform container;
        [SerializeField] List<Group> enemiesList;
        [SerializeField, Min(0.1f)] float minInterval;
        [SerializeField, Min(0.1f)] float maxInterval;

        int _remaining;

        void Start()
        {
            foreach (Group enemies in enemiesList) _remaining += enemies.count;
            
            GenerateAnEnemy();
        }

        async void GenerateAnEnemy()
        {
            while (_remaining > 0)
            {
                int index = Random.Range(0, enemiesList.Count);
                
                if (enemiesList[index].count <= 0)
                {
                    enemiesList.RemoveAt(index);
                    continue;
                }
                
                enemiesList[index].count--;
                _remaining--;
                Instantiate(enemiesList[index].enemy, GetSpawnPoint(), Quaternion.identity, container);
                await Awaitable.WaitForSecondsAsync(Random.Range(minInterval, maxInterval));
            }
            
            enemiesList.Clear();
        }
        
        Vector2 GetSpawnPoint() => Random.Range(0, 2) == 0 ? leftSpawn.position : rightSpawn.position;
    } 
}