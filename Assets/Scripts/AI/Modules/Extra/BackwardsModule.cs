using UnityEngine;
using Game = Player.PlayerController;

namespace AI.Modules.Extra
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
			if (controller.health.hit)
			{
				SetDestination();
				controller.health.hit = false;
			} 
			if (_retreating) transform.position = Vector2.MoveTowards(transform.position, _target, speed * Time.deltaTime);
		}

		async void SetDestination()
		{
			controller.EnableModules(false);
			_target = Game.Player.transform.position + new Vector3(distance * controller.movement.stop, 0);
			_retreating = true;
			await Awaitable.WaitForSecondsAsync(duration);
			_retreating = false;
			if(!controller.health.live) return;
			controller.EnableModules();
			done = true;
		}
	}
}