using Gameplay.Player.Modules;
using UnityEngine;

namespace Gameplay.Player
{
[RequireComponent(typeof(AttackModule), typeof(HealthModule))]
	public class PlayerController : MonoBehaviour
	{
		public static PlayerController player { get; private set; }

		internal AttackModule attack;
		internal HealthModule health;
		internal Animator animator;

		void Awake()
		{
			player = this;
			animator = GetComponent<Animator>();
			attack = GetComponent<AttackModule>();
			health = GetComponent<HealthModule>();
		}
	}
}
