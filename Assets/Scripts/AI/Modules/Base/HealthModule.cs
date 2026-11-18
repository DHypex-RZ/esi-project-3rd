using TMPro;
using UnityEngine;
using Game = Player.PlayerController;
using static Gameplay.ScoreController;

namespace AI.Modules.Base
{
	public class HealthModule: ModuleController
	{
		[SerializeField, Min(0)] internal int healthPoints;
		[SerializeField] TMP_Text points;
		[SerializeField] ParticleSystem particles;

		internal bool hit;
		internal bool live => healthPoints > 0;
		
		protected override void Awake()
		{
			base.Awake();
			points.text = healthPoints.ToString();
			particles.Stop();
		}

		public void ReceiveAttack(bool instakill = false)
		{
			healthPoints--;
			points.text = healthPoints.ToString();
			hit = true;
			particles.Play();
			
			if (!instakill)
			{
				if (live) return;
				Game.Player.special.UpdateCount();
			}
			
			controller.EnableModules(false);
			Score.UpdateScore(controller.score, false);
			Score.UpdateCount();
			Destroy(gameObject);
		}
	}
}