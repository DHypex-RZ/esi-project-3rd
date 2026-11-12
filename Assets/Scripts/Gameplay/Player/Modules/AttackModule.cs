using Enemy = Gameplay.AI.Modules.Base;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Gameplay.Player.Modules
{
	public class AttackModule: ModuleController
	{
		[SerializeField, Min(0)] float cooldown;
		[SerializeField, Min(0)] float duration;
		[SerializeField, Min(0)] float fail;
		[SerializeField, Min(0)] float rayLenght;
		[SerializeField] LayerMask layer;
		

		[Header("Left")]
		[SerializeField] Button leftButton;
		EventTrigger _leftTrigger;

		[Header("Right")]
		[SerializeField] Button rightButton;
		EventTrigger _rightTrigger;

		Button _buttonSelected;
		EventTrigger _trigger;
		bool _attacking;

		public void AttackOnLeft() => Attack(Vector2.left, leftButton, _leftTrigger);
		public void AttackOnRight() => Attack(Vector2.right, rightButton, _rightTrigger);
		void EnableButton() => _buttonSelected.interactable = _trigger.enabled = true;

		protected override void Awake()
		{
			base.Awake();
			_leftTrigger = leftButton.GetComponent<EventTrigger>();
			_rightTrigger = rightButton.GetComponent<EventTrigger>();
		}

		void Attack(Vector2 direction, Button button, EventTrigger trigger)
		{
			_buttonSelected = button;
			_trigger = trigger;
			trigger.enabled = button.interactable = false;
			controller.animation.Play("Attacking");
			FindEnemy(direction);
		}
		
	 	void FindEnemy(Vector2 direction)
		{
			RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, rayLenght, layer);
			Debug.DrawRay(transform.position, direction * rayLenght, Color.red, 1);

			if (hit && hit.collider.TryGetComponent(out Enemy.HealthModule health))
			{
				health.ReceiveAttack();
				return;
			}

			hit = Physics2D.Raycast(transform.position, -direction, rayLenght, layer);
			if (hit && hit.collider.TryGetComponent(out Enemy.AttackModule attack)) attack.PunishPlayer();
		}
	}
}