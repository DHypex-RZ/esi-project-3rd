using UnityEngine;
using Game = Player.PlayerController;

namespace AI.Modules.Base
{
	public class AttackModule: ModuleController
	{
		[SerializeField, Min(0)] internal int damage;
		[SerializeField, Min(0)] float cooldown;

		bool _attacking;

		protected void Update() { if (controller.movement.inFrontOfPlayer && !_attacking) Attack(); }

		async void Attack()
		{
			_attacking = true;
			await Awaitable.WaitForSecondsAsync(cooldown);

			if(!controller.health.live || !controller.movement.inFrontOfPlayer || !Game.Player.health.live) return;

			PunishPlayer();
			_attacking = false;
		}

		internal void PunishPlayer() => controller.animator.Play("Attacking");
	}
}