using UnityEngine;
using Game = Player.PlayerController;

namespace AI.Modules.Base
{
	public class MovementModule: ModuleController
	{
		[SerializeField, Min(0)] float speed;
		[SerializeField, Min(0.1f)] float reductionByHp; 
		[SerializeField, Min(0)] internal float stop;
		[SerializeField] LayerMask layer;

		internal bool inFrontOfPlayer { get; private set; }

		Vector2 _target;

		void Start()
		{
			speed -= controller.health.healthPoints * reductionByHp;
			if (transform.position.x < Game.Player.transform.position.x) InvertValues();
		}

		void Update()
		{
			inFrontOfPlayer = Mathf.Approximately(Vector2.Distance(transform.position, Game.Player.transform.position), Mathf.Abs(stop) + Game.Player.box.size.x / 2);
		}

		void FixedUpdate()
		{
			if (FindForward(out RaycastHit2D other)) _target = other.point + new Vector2(stop, 0); 
			else _target = Game.Player.transform.position + new Vector3(stop, 0);

			transform.position = Vector2.MoveTowards(transform.position, _target, speed * Time.fixedDeltaTime);
		}

		bool FindForward(out RaycastHit2D hit)
		{
			hit = Physics2D.Raycast(transform.position, Game.Player.transform.position - transform.position, Mathf.Infinity, layer);
			return hit.collider;
		}

		internal void InvertValues()
		{
			controller.sprite.flipX = !controller.sprite.flipX;
			stop = -stop;
		}
	}
}