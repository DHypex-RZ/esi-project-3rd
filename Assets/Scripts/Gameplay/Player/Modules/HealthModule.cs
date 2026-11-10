using UnityEngine;
using static UI.InterfaceController;

namespace Gameplay.Player.Modules
{
	public class HealthModule: ModuleController
	{
		[SerializeField] int health;
		
		float _currentHealth;

		public bool live => _currentHealth > 0;

		protected override void Awake()
		{
			base.Awake();
			_currentHealth = health;
		}
		
		public void ReceiveDamage(int damage)
		{
			controller.animator.Play("Damaged");
			_currentHealth -= damage;
			canvas.UpdateHealth(health, live ? _currentHealth : 0);
			if (!live) canvas.RetryMenu();
		}
	}
}