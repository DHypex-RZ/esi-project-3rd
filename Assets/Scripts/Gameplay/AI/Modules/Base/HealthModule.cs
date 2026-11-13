using UnityEngine;
using static Gameplay.LevelController;

namespace Gameplay.AI.Modules.Base
{
	public class HealthModule: ModuleController
	{
		[SerializeField, Min(0)] internal int healthPoints;

		internal bool hit;
		internal bool live => healthPoints > 0;

		public void ReceiveAttack()
		{
			healthPoints--;
			hit = true;
			if (live) return;
			level.UpdateScore(ctl.score, false);
			Destroy(gameObject);
		}
	}
}