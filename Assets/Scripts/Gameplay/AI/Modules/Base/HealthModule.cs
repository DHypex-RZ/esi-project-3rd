using UnityEngine;

namespace Gameplay.AI.Modules.Base
{
	public class HealthModule: ModuleController
	{
		[SerializeField, Min(0)] internal int lifePoints;

		internal bool hit;
		internal bool live => lifePoints > 0;

		public void ReceiveAttack()
		{
			lifePoints--;
			hit = true;
			
			if (live) return;
			
			Destroy(gameObject);
		}
	}
}