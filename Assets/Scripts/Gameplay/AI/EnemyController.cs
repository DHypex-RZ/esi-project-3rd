using System;
using Gameplay.AI.Modules.Base;
using UnityEngine;
using static Gameplay.Player.PlayerController;

namespace Gameplay.AI
{
	[RequireComponent(typeof(MovementModule), typeof(AttackModule), typeof(ScoreModule))]
	public class EnemyController : MonoBehaviour
	{
		internal MovementModule movement { get; private set; }
		internal ScoreModule score { get; private set; }
		internal SpriteRenderer sprite { get; private set; }
		internal Animator animator { get; private set; }
		AttackModule attack { get; set; }
		new Collider2D collider { get; set; }

		void Awake()
		{
			movement = GetComponent<MovementModule>();
			attack = GetComponent<AttackModule>();
			score = GetComponent<ScoreModule>();
			sprite = GetComponent<SpriteRenderer>();
			animator = GetComponent<Animator>();
			collider = GetComponent<Collider2D>();
		}

		void Update() { if(!player.health.live) EnableModules(false); }

		internal void EnableModules(bool on = true) => collider.enabled = movement.enabled = attack.enabled = score.enabled = on;

		[Serializable] public class Group
		{
			[SerializeField] public GameObject enemy;
			[SerializeField, Min(1)] public int count;
		}
	}
}