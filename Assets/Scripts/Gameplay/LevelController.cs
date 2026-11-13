using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Enemy = Gameplay.AI.EnemyController;
using static Gameplay.Player.PlayerController;
using static UI.MenuController;
using Random = UnityEngine.Random;

namespace Gameplay
{
	public class LevelController : MonoBehaviour
	{
		internal static LevelController level;

		[Header("Settings")]
		[SerializeField] Transform leftSpawn;
		[SerializeField] Transform rightSpawn;
		[SerializeField] Transform container;
		[SerializeField] List<Enemy.Group> enemiesList;
		[SerializeField, Min(0.1f)] float minInterval;
		[SerializeField, Min(0.1f)] float maxInterval;
		[Header("Interface")]
		[SerializeField] Text score;
		[SerializeField] Text multiplier;
		[SerializeField] Text count;

		int _score;
		float _multiplier = 0.5f;
		int _enemyCount;
		int _remaining;
		bool _checkConditions;
		
		Vector2 GetSpawnPoint() => Random.Range(0, 2) == 0 ? leftSpawn.position : rightSpawn.position;

		void Awake() => level = this;

		void Start()
		{
			foreach (Enemy.Group enemies in enemiesList) _remaining += enemies.count;
			count.text = _remaining.ToString();
			GenerateEnemies();
		}

		void Update()
		{
			if (!_checkConditions) return;
			if(player.hlt.live && container.childCount == 0) hud.WinMenu();
		}

		async void GenerateEnemies()
		{
			while (_remaining > 0 && player.hlt.live)
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
			_checkConditions = true;
		}
		
		internal void UpdateScore(int score = 0, bool reset = true)
		{
			if (reset) _enemyCount = 0; 
			else _enemyCount++; 
			
			_multiplier = 1 + _enemyCount / 5 * 0.5f;
			multiplier.text = "x " + _multiplier;
			_score += Mathf.RoundToInt(score * _multiplier);
			this.score.text = _score.ToString();
		}
	}
}