using UnityEngine;
using static Gameplay.Player.PlayerController;

namespace Gameplay.AI.Modules.Extra
{
	public class SlidingModule: AdvanceModuleController
	{
		[SerializeField, Min(0)] float speed;
		[SerializeField, Min(0)] float duration;

		Vector2 _target;
		bool _sliding;

		void Update()
		{
			if (ctl.hlt.hit)
			{
				SetDestination();
				ctl.hlt.hit = false;
			}
			if (_sliding) Slide();
		}

		void Slide() { transform.position = Vector2.MoveTowards(transform.position, _target, speed * Time.deltaTime); }

		async void SetDestination()
		{
			ctl.EnableModules(false);
			ctl.mvt.InvertValues();
			_target = player.transform.position + new Vector3(ctl.mvt.stop, 0);
			_sliding = true;
			await Awaitable.WaitForSecondsAsync(duration);
			_sliding = false;
			if(!ctl.hlt.live) return;
			ctl.EnableModules();
			done = true;
		}
	}
}