using System;
using UnityEngine;

namespace Gameplay.AI.Modules
{
	[Serializable, RequireComponent(typeof(EnemyController))]
	public abstract class ModuleController: MonoBehaviour
	{
		protected EnemyController ctl { get; private set; }

		protected virtual void Awake() => ctl = GetComponent<EnemyController>();
	}
}