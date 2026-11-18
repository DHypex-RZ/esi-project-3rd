using UnityEngine;
using UnityEngine.UI;
using static Gameplay.ScoreController;
using static UI.MenuController;

namespace Player.Modules
{
	[RequireComponent(typeof(BoxCollider2D))]
	public class HealthModule: ModuleController
	{
		[SerializeField] int health;
		[SerializeField] Image healthBar;
		[SerializeField] ParticleSystem particles;
		
		float _currentHealth;

		internal bool live => _currentHealth > 0;

		protected override void Awake()
		{
			base.Awake();
			particles.Stop();
			_currentHealth = health;
		}
		
		public void ReceiveDamage(int damage)
		{
			particles.Play();
			controller.animator.Play("Hurt");
			_currentHealth -= damage;
			Score.UpdateScore();
			healthBar.fillAmount = live ? _currentHealth / health : 0;
			if (!live) Menu.RetryMenu();
		}
	}
}