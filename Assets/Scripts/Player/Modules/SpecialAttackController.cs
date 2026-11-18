using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Enemy = AI.Modules.Base;

namespace Player.Modules
{
	public class SpecialAttackController: ModuleController
	{
		[SerializeField] int count;
		[SerializeField] Button leftButton;
		[SerializeField] Button rightButton;
		[SerializeField] Image indicator; 

		int _count;
		EventTrigger _leftTrigger;
		EventTrigger _rightTrigger;
		
		
		public void AttackOnLeft() => Attack(Vector2.left, leftButton, _leftTrigger);
		public void AttackOnRight() => Attack(Vector2.right, rightButton, _rightTrigger);
		
		protected override void Awake()
		{
			base.Awake();
			_leftTrigger = leftButton.GetComponent<EventTrigger>();
			_rightTrigger = rightButton.GetComponent<EventTrigger>();
			EnableButtons(false);
		}
		public void UpdateCount()
		{
			if (_count >= count) return;
			
			_count++;
			indicator.fillAmount = (float) _count / count;
			
			if (_count ==  count) EnableButtons(true);
		}
		
		void EnableButtons(bool enabled) => rightButton.interactable = _rightTrigger.enabled = leftButton.interactable = _leftTrigger.enabled = enabled;
		
		void Attack(Vector2 direction, Button button, EventTrigger trigger)
		{
			trigger.enabled = button.interactable = false;
			controller.animator.Play("Attack");
			_count = 0;
			EnableButtons(false);
			KillAll(direction);
		}

		void KillAll(Vector2 direction)
		{
			RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, direction, Mathf.Infinity, controller.attack.layer);
			foreach (RaycastHit2D hit in hits)
			{
				if (hit.collider.TryGetComponent(out Enemy.HealthModule enemy)) enemy.ReceiveAttack(true);
			}
		}
	}
}