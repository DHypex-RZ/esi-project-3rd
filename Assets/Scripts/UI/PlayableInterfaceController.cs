using Gameplay.Player;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PlayableInterfaceController : MonoBehaviour
    {
        [SerializeField] PlayerController player;

        public void PauseMenu()
        {
            Debug.Log("Pause Menu");
        }
        
        public void OnAttackButton(bool right)
        {
            player.Attack(right);
        }
    }
}
