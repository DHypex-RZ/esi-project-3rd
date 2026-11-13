using UnityEngine;
using static Gameplay.Player.PlayerController;

namespace Gameplay.AI.Modules.Extra
{
	public class BackwardsModule: AdvanceModuleController
	{
		[SerializeField, Min(0)] float speed;
		[SerializeField, Min(0)] float duration;
		[SerializeField, Min(0)] float distance;

		Vector2 _target;
		bool _retreating;

		void Update()
		{
			if (ctl.hlt.hit)
			{
				SetDestination();
				ctl.hlt.hit = false;
			} 
			if (_retreating) transform.position = Vector2.MoveTowards(transform.position, _target, speed * Time.deltaTime);
		}

		async void SetDestination()
		{
			ctl.EnableModules(false);
			_target = player.transform.position + new Vector3(distance * ctl.mvt.stop, 0);
			_retreating = true;
			await Awaitable.WaitForSecondsAsync(duration);
			_retreating = false;
			if(!ctl.hlt.live) return;
			ctl.EnableModules();
			done = true;
		}
	}
}