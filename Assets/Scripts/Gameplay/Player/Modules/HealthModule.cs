using UnityEngine;
using UnityEngine.UI;
using static Gameplay.LevelController;
using static UI.MenuController;

namespace Gameplay.Player.Modules
{
	public class HealthModule: ModuleController
	{
		[SerializeField] int health;
		[SerializeField] Image healthBar;
		
		float _currentHealth;

		internal bool live => _currentHealth > 0;

		protected override void Awake()
		{
			base.Awake();
			_currentHealth = health;
		}
		
		public void ReceiveDamage(int damage)
		{
			ctl.animator.Play("Hurt");
			_currentHealth -= damage;
			level.UpdateScore();
			healthBar.fillAmount = live ? _currentHealth / health : 0;
			if (!live) hud.RetryMenu();
		}
	}
}