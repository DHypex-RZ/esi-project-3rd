using System;
using UnityEngine;

namespace Gameplay.Player.Modules
{
	[Serializable, RequireComponent(typeof(PlayerController))]
	public abstract class ModuleController: MonoBehaviour
	{
		protected PlayerController ctl;

		protected virtual void Awake() => ctl = GetComponent<PlayerController>();
	}
}