using System.Collections.Generic;
using Game = Player.PlayerController;
using UnityEngine;
using Enemy = AI.EnemyController;
using static UI.MenuController;
using Random = UnityEngine.Random;

namespace Gameplay
{
	public class GeneratorController : MonoBehaviour
	{
		internal static GeneratorController Generator { get; private set; }
		
		[SerializeField] Transform leftSpawn;
		[SerializeField] Transform rightSpawn;
		[SerializeField] Transform container;
		[SerializeField] List<Enemy.Group> enemiesList;
		[SerializeField, Min(0.1f)] float minInterval;
		[SerializeField, Min(0.1f)] float maxInterval;

		internal int remaining;
		
		int _enemyCount;
		bool _checkConditions;
		
		Vector2 GetSpawnPoint() => Random.Range(0, 2) == 0 ? leftSpawn.position : rightSpawn.position;

		void Awake() {
			Generator = this;
			foreach (Enemy.Group enemies in enemiesList) remaining += enemies.count;
		}

		void Start() => GenerateEnemies();

		void Update()
		{
			if (!_checkConditions) return;
			if(Game.Player.health.live && container.childCount == 0) Menu.WinMenu();
		}

		async void GenerateEnemies()
		{
			while (remaining > 0 && Game.Player.health.live)
			{
				int index = Random.Range(0, enemiesList.Count);

				if (enemiesList[index].count <= 0)
				{
					enemiesList.RemoveAt(index);
					continue;
				}

				enemiesList[index].count--;
				remaining--;
				Instantiate(enemiesList[index].enemy, GetSpawnPoint(), Quaternion.identity, container);
				await Awaitable.WaitForSecondsAsync(Random.Range(minInterval, maxInterval));
			}

			enemiesList.Clear();
			_checkConditions = true;
		}
	}
}