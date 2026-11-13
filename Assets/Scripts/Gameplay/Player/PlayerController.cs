using Gameplay.Player.Modules;
using UnityEngine;

namespace Gameplay.Player
{
[RequireComponent(typeof(Animator),typeof(AttackModule), typeof(HealthModule))]
	public class PlayerController : MonoBehaviour
	{
		public static PlayerController player { get; private set; }
		
		internal AttackModule atk {get; private set;}
		internal HealthModule hlt {get; private set;}
		public BoxCollider2D box {get; private set;}
		public Animator animator {get; private set;}
		
		void Awake()
		{
			player = this;
			atk = GetComponent<AttackModule>();
			hlt = GetComponent<HealthModule>();
			box = GetComponent<BoxCollider2D>();
			animator = GetComponent<Animator>();
		}
	}
}