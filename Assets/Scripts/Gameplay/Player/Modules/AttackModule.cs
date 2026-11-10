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

		[Header("Left")]
		[SerializeField] Collider2D leftHitbox;
		[SerializeField] Button leftButton;
		EventTrigger _leftTrigger;

		[Header("Right")]
		[SerializeField] Collider2D rightHitbox;
		[SerializeField] Button rightButton;
		EventTrigger _rightTrigger;

		public bool hit;

		public void AttackOnLeft() => Attack(leftHitbox, leftButton, _leftTrigger);
		public void AttackOnRight() => Attack(rightHitbox, rightButton, _rightTrigger);

		protected override void Awake()
		{
		base.Awake();
			_leftTrigger = leftButton.GetComponent<EventTrigger>();
			_rightTrigger = rightButton.GetComponent<EventTrigger>();
		}

		async void Attack(Collider2D area, Button button, EventTrigger trigger)
		{
			trigger.enabled = button.interactable = false;
			controller.animator.Play("Attacking");
			area.enabled = true;
			await Awaitable.WaitForSecondsAsync(duration);
			if(hit) await Awaitable.WaitForSecondsAsync(cooldown - duration);
			else await Awaitable.WaitForSecondsAsync(fail - duration);
			area.enabled = hit = false;
			trigger.enabled = button.interactable = true;
		}
	}
}