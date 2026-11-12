using Gameplay.Player.Modules;
using UnityEngine;

namespace Gameplay.Player
{
[RequireComponent(typeof(AttackModule), typeof(HealthModule))]
	public class PlayerController : MonoBehaviour
	{
		public static PlayerController player { get; private set; }

		[SerializeField] Animator animator;
		
		public AttackModule attack {get; private set;}
		public HealthModule health {get; private set;}
		public new BoxCollider2D collider {get; private set;}
		public new Animator animation {get; private set;}
		
		void Awake()
		{
			player = this;
			animator = GetComponent<Animator>();
			collider = GetComponent<BoxCollider2D>();
			attack = GetComponent<AttackModule>();
			health = GetComponent<HealthModule>();
			animation = animator;
		}
	}
}
