using UnityEngine;
using Game = Player.PlayerController;

namespace AI.Modules.Extra
{
	public class SlidingModule: AdvanceModuleController
	{
		[SerializeField, Min(0)] float speed;
		[SerializeField, Min(0)] float duration;

		Vector2 _target;
		bool _sliding;

		void Update()
		{
			if (controller.health.hit)
			{
				SetDestination();
				controller.health.hit = false;
			}
			if (_sliding) Slide();
		}

		void Slide() { transform.position = Vector2.MoveTowards(transform.position, _target, speed * Time.deltaTime); }

		async void SetDestination()
		{
			controller.EnableModules(false);
			controller.movement.InvertValues();
			_target = Game.Player.transform.position + new Vector3(controller.movement.stop, 0);
			_sliding = true;
			await Awaitable.WaitForSecondsAsync(duration);
			_sliding = false;
			if(!controller.health.live) return;
			controller.EnableModules();
			done = true;
		}
	}
}