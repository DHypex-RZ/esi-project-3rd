using System;
using Gameplay.AI.Modules.Base;
using static Gameplay.Player.PlayerController;
using UnityEngine;

namespace Gameplay.AI
{
	[RequireComponent(typeof(MovementModule), typeof(AttackModule), typeof(HealthModule))]
	public class EnemyController : MonoBehaviour
	{
		[SerializeField, Min(0)] internal int score;
		
		internal MovementModule mvt { get; private set; }
		internal HealthModule hlt { get; private set; }
		internal AttackModule atk { get; private set; }
		internal SpriteRenderer sprite { get; private set; }
		internal Animator animator { get; private set; }

		void Awake()
		{
			mvt = GetComponent<MovementModule>();
			atk = GetComponent<AttackModule>();
			hlt = GetComponent<HealthModule>();
			sprite = GetComponent<SpriteRenderer>();
			animator = GetComponent<Animator>();
		}

		void Update() { if(!player.hlt.live) EnableModules(false); }
		
		internal void EnableModules(bool on = true) => mvt.enabled = atk.enabled = hlt.enabled = on;
		void AttackPlayer() => player.hlt.ReceiveDamage(atk.damage);

		[Serializable] public class Group
		{
			[SerializeField] public GameObject enemy;
			[SerializeField, Min(1)] public int count;
		}
	}
}