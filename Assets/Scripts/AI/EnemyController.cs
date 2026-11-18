using System;
using AI.Modules.Base;
using Game = Player.PlayerController;
using UnityEngine;

namespace AI
{
	[RequireComponent(typeof(MovementModule), typeof(AttackModule), typeof(HealthModule))]
	public class EnemyController : MonoBehaviour
	{
		[SerializeField, Min(0)] internal int score;
		
		internal MovementModule movement { get; private set; }
		internal HealthModule health { get; private set; }
		internal AttackModule attack { get; private set; }
		internal SpriteRenderer sprite { get; private set; }
		internal Animator animator { get; private set; }

		void Awake()
		{
			movement = GetComponent<MovementModule>();
			attack = GetComponent<AttackModule>();
			health = GetComponent<HealthModule>();
			sprite = GetComponent<SpriteRenderer>();
			animator = GetComponent<Animator>();
		}

		void Update() { if(!Game.Player.health.live) EnableModules(false); }
		
		internal void EnableModules(bool enable = true) => movement.enabled = attack.enabled = health.enabled = enable;
		void AttackPlayer() => Game.Player.health.ReceiveDamage(attack.damage);

		[Serializable] public class Group
		{
			[SerializeField] public GameObject enemy;
			[SerializeField, Min(1)] public int count;
		}
	}
}