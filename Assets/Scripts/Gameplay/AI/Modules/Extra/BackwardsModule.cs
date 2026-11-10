using UnityEngine;
using static Gameplay.Player.PlayerController;

namespace Gameplay.AI.Modules.Extra
{
	public class BackwardsModule: AdvanceModuleController
	{
		[SerializeField, Min(0)] float speed;
		[SerializeField, Min(0)] float duration;
		[SerializeField, Min(0)] float distance;
		[SerializeField] string bypass;

		int _mask;
		Vector2 _target;
		bool _retreating;

		protected override void Awake()
		{
			base.Awake();
			_mask = gameObject.layer;
		}

		void Update() { if (_retreating) transform.position = Vector2.MoveTowards(transform.position, _target, speed * Time.deltaTime); }

		async void SetDestination()
		{
			gameObject.layer = LayerMask.NameToLayer(bypass);
			controller.EnableModules(false);
			_target = player.transform.position + new Vector3(distance * controller.movement.stop + controller.movement.gap, 0);
			_retreating = true;
			await Awaitable.WaitForSecondsAsync(duration);
			_retreating = false;
			controller.EnableModules();
			gameObject.layer = _mask;
			done = true;
		}

		void OnTriggerEnter2D(Collider2D other) { if (enabled && other.CompareTag("PlayerAttack") && controller.score.live) SetDestination(); }
	}
}