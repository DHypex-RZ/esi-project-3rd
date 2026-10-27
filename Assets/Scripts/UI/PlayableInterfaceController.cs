using Gameplay.Player;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PlayableInterfaceController : MonoBehaviour
    {
        [SerializeField] PlayerController player;
        
        //[SerializeField] Button btnJump;
        //[SerializeField] Button btnSlash;
        //[SerializeField] Button btnDefend;
        
        void Start()
        {
            
        }

        void Update()
        {
        }

        public void PauseMenu()
        {
            Debug.Log("Pause Menu");
        }
    }
}
