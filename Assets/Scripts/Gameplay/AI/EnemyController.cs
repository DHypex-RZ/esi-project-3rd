using System;
using Gameplay.AI.Modules.Base;
using UnityEngine;
using static Gameplay.Player.PlayerController;

namespace Gameplay.AI
{
	[RequireComponent(typeof(MovementModule), typeof(AttackModule), typeof(HealthModule))]
	public class EnemyController : MonoBehaviour
	{
		/*static float _multiplier;
		static int totalScore { get; set; }
		static int count { get; set; }*/
		
		[SerializeField, Min(0)] int score;
		
		internal MovementModule movement { get; private set; }
		internal HealthModule health { get; private set; }
		internal SpriteRenderer sprite { get; private set; }
		internal Animator animator { get; private set; }
		AttackModule attack { get; set; }
		new Collider2D collider { get; set; }

		void Awake()
		{
			movement = GetComponent<MovementModule>();
			attack = GetComponent<AttackModule>();
			health = GetComponent<HealthModule>();
			sprite = GetComponent<SpriteRenderer>();
			animator = GetComponent<Animator>();
			collider = GetComponent<Collider2D>();
		}

		void Update() { if(!player.health.live) EnableModules(false); } // CalculateScore(); canvas.UpdateScore(totalScore);
		
		/*void CalculateScore()
		{
			count++;
			_multiplier = 1 + count / 5 * 0.5f;
			totalScore += Mathf.RoundToInt(score * _multiplier);
			canvas.UpdateMultiplier(_multiplier);
		}*/

		internal void EnableModules(bool on = true) => collider.enabled = movement.enabled = attack.enabled = health.enabled = on;

		[Serializable] public class Group
		{
			[SerializeField] public GameObject enemy;
			[SerializeField, Min(1)] public int count;
		}
	}
}