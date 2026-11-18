using UnityEngine;
using UnityEngine.UI;
using Game = Player.PlayerController;

namespace UI
{
	public class DisplayController: MonoBehaviour
	{
		[SerializeField] Image leftIndicator;
		[SerializeField] Image rightIndicator;
		
		void Update()
		{
			leftIndicator.color = EnemyOnRange(Vector2.left) ? Color.red : Color.gray;
			rightIndicator.color = EnemyOnRange(Vector2.right) ? Color.red : Color.gray;
		}
		
		bool EnemyOnRange(Vector2 direction) => Physics2D.Raycast(Game.Player.transform.position, direction, Game.Player.attack.rayLenght, Game.Player.attack.layer).collider;
	}
}