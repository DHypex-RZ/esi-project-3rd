using UnityEngine;
using UnityEngine.UI;
using static UI.InterfaceController;

namespace Gameplay.Player.Modules
{
	public class HealthModule: ModuleController
	{
		[SerializeField] int health;
		[SerializeField] Image frame;
		
		float _currentHealth;

		internal bool live => _currentHealth > 0;

		protected override void Awake()
		{
			base.Awake();
			_currentHealth = health;
		}
		
		internal void ReceiveDamage(int damage)
		{
			controller.animator.Play("Damaged");
			_currentHealth -= damage;
			frame.fillAmount = live ? _currentHealth / health : 0;
			if (!live) canvas.RetryMenu();
		}
	}
}