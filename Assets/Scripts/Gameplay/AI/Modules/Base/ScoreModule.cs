using UnityEngine;
using static UI.InterfaceController;
using static Gameplay.Player.PlayerController;

namespace Gameplay.AI.Modules.Base
{
	public class ScoreModule: ModuleController
	{
		public static int totalScore { get; private set; }
		static float _multiplier;

		public static int count { private get; set; }

		[SerializeField, Min(0)] internal int hitPoints;
		[SerializeField, Min(0)] int score;

		internal bool live => hitPoints > 0;

		void BeingAttacked()
		{
			hitPoints--;
			player.attack.hit = true;

			if (live) return;

			CalculateScore();
			canvas.UpdateScore(totalScore);
			Destroy(gameObject);
		}

		void CalculateScore()
		{
			count++;
			_multiplier = 1 + count / 5 * 0.5f;
			totalScore += Mathf.RoundToInt(score * _multiplier);
			canvas.UpdateMultiplier(_multiplier);
		}

		void OnTriggerEnter2D(Collider2D other) { if (other.CompareTag("PlayerAttack")) BeingAttacked(); }
	}
}