using System;
using UnityEngine;

namespace Gameplay.AI.Modules
{
	[Serializable, RequireComponent(typeof(EnemyController))]
	public abstract class ModuleController: MonoBehaviour
	{
		protected EnemyController controller { get; private set; }

		protected virtual void Awake() => controller = GetComponent<EnemyController>();
	}
}