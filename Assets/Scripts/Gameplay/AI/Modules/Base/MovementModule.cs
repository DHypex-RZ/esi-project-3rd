using UnityEngine;
using static Gameplay.Player.PlayerController;

namespace Gameplay.AI.Modules.Base
{
	public class MovementModule: ModuleController
	{
		[SerializeField, Min(0)] float speed;
		[SerializeField, Min(0)] internal float stop;
		[SerializeField] LayerMask layer;

		internal bool inFrontOfPlayer => Mathf.Approximately(Vector2.Distance(transform.position, player.transform.position), Mathf.Abs(stop + gap));
		internal float gap = .5f;

		Vector2 _target;

		void Start() { if (transform.position.x < player.transform.position.x) InvertValues(); }

		void FixedUpdate()
		{
			if (FindForward(out RaycastHit2D other)) _target = other.point + new Vector2(stop, 0); 
			else _target = player.transform.position + new Vector3(stop, 0);

			transform.position = Vector2.MoveTowards(transform.position, _target, speed * Time.fixedDeltaTime);
		}

		bool FindForward(out RaycastHit2D hit)
		{
			hit = Physics2D.Raycast(transform.position, player.transform.position - transform.position, Mathf.Infinity, layer);
			return hit.collider;
		}

		internal void InvertValues()
		{
			controller.sprite.flipX = !controller.sprite.flipX;
			stop = -stop;
			gap = -gap;
		}
	}
}