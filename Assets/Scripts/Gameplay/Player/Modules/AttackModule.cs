using Enemy = Gameplay.AI.Modules.Base;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Gameplay.Player.Modules
{
	public class AttackModule: ModuleController
	{
		[SerializeField, Min(0)] float rayLenght;
		[SerializeField] LayerMask layer;
		[SerializeField] Button leftButton;
		[SerializeField] Image leftIndicator;
		[SerializeField] Button rightButton;
		[SerializeField] Image rightIndicator;
		
		EventTrigger _leftTrigger;
		EventTrigger _rightTrigger;
		Button _selectedButton;
		EventTrigger _selectedTrigger;

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

		void Update()
		{
			leftIndicator.color = EnemyOnRange(Vector2.left) ? Color.red : Color.gray;
			rightIndicator.color = EnemyOnRange(Vector2.right) ? Color.red : Color.gray;
			
		}

		void Attack(Vector2 direction, Button button, EventTrigger trigger)
		{
			_selectedButton = button;
			_selectedTrigger = trigger;
			trigger.enabled = button.interactable = false;
			ctl.animator.Play("Attack");
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
			if (hit && hit.collider.TryGetComponent(out Enemy.AttackModule attack)) attack.PunishPlayer();
		}

		bool EnemyOnRange(Vector2 direction) => Physics2D.Raycast(transform.position, direction, rayLenght, layer).collider;
	}
}