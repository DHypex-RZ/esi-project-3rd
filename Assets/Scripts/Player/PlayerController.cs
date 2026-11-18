using Player.Modules;
using UnityEngine;

namespace Player
{
[RequireComponent(typeof(PrimaryAttackModule), typeof(HealthModule), typeof(SpecialAttackController))]
	public class PlayerController : MonoBehaviour
	{
		public static PlayerController Player { get; private set; }

		internal PrimaryAttackModule attack { get; private set; }
		internal SpecialAttackController special { get; private set; }
		internal HealthModule health { get; private set; }
		public BoxCollider2D box { get; private set; }
		public Animator animator { get; private set; }
		
		void Awake()
		{
			Player = this;
			attack = GetComponent<PrimaryAttackModule>();
			special = GetComponent<SpecialAttackController>();
			health = GetComponent<HealthModule>();
			box = GetComponent<BoxCollider2D>();
			animator = GetComponent<Animator>();
		}
	}
}