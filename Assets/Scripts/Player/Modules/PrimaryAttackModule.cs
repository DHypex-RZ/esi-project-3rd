using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Enemy = AI.Modules.Base;

namespace Player.Modules
{
	public class PrimaryAttackModule: ModuleController
	{
		[SerializeField, Min(0)] internal float rayLenght;
		[SerializeField] internal LayerMask layer;
		[SerializeField] Button leftButton;
		[SerializeField] Button rightButton;
		
		EventTrigger _leftTrigger;
		EventTrigger _rightTrigger;
		Button _selectedButton;
		EventTrigger _selectedTrigger;
		bool _isPunished;

		public void AttackOnLeft() => Attack(Vector2.left, leftButton, _leftTrigger);
		public void AttackOnRight() => Attack(Vector2.right, rightButton, _rightTrigger);
		void EnableButton() => _selectedButton.interactable = _selectedTrigger.enabled = true;

		protected override void Awake()
		{
			base.Awake();
			_leftTrigger = leftButton.GetComponent<EventTrigger>();
			_selectedTrigger = _rightTrigger = rightButton.GetComponent<EventTrigger>();
			_selectedButton = rightButton;
		}

		void Attack(Vector2 direction, Button button, EventTrigger trigger)
		{
			EnableButton();
			_selectedButton = button;
			_selectedTrigger = trigger;
			trigger.enabled = button.interactable = false;
			controller.animator.Play("Attack");
			FindEnemy(direction);
		}
		
	 	void FindEnemy(Vector2 direction)
		{
			RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, rayLenght, layer);

			if (hit && hit.collider.TryGetComponent(out Enemy.HealthModule health))
			{
				health.ReceiveAttack();
				return;
			}

			hit = Physics2D.Raycast(transform.position, -direction, rayLenght, layer);
			
			if (!hit || !hit.collider.TryGetComponent(out Enemy.AttackModule attack) || _isPunished) return;
			
			attack.PunishPlayer();
			_isPunished = true;
			CooldownToPunish();
		}

		async void CooldownToPunish()
		{
			await Awaitable.WaitForSecondsAsync(.5f);
			_isPunished = false;
		}
	}
}