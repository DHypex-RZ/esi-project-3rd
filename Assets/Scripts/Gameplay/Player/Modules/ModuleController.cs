using System;
using UnityEngine;

namespace Gameplay.Player.Modules
{
	[Serializable, RequireComponent(typeof(PlayerController))]
	public abstract class ModuleController: MonoBehaviour
	{
		protected PlayerController controller;

		protected virtual void Awake() => controller = GetComponent<PlayerController>();
	}
}