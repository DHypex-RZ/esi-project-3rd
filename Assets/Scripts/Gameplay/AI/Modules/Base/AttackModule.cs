using UnityEngine;
using static Gameplay.Player.PlayerController;

namespace Gameplay.AI.Modules.Base
{
	public class AttackModule: ModuleController
	{
		[SerializeField, Min(0)] internal int damage;
		[SerializeField, Min(0)] float cooldown;

		bool _attacking;

		protected void Update() { if (ctl.mvt.inFrontOfPlayer && !_attacking) Attack(); }

		async void Attack()
		{
			_attacking = true;
			await Awaitable.WaitForSecondsAsync(cooldown);

			if(!ctl.hlt.live || !ctl.mvt.inFrontOfPlayer || ctl.hlt.hit || !player.hlt.live) return;

			PunishPlayer();
			_attacking = false;
		}

		internal void PunishPlayer() => ctl.animator.Play("Attacking");
	}
}