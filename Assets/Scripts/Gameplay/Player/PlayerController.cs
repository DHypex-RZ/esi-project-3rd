using UnityEngine;

namespace Gameplay.Player
{
    public class PlayerController : MonoBehaviour
    {
        public static Transform player { get; private set; }

        void Awake() => player = transform;
        
        public async void Attack(GameObject area)
        {
            area.SetActive(true);
            await Awaitable.WaitForSecondsAsync(0.1f);
            area.SetActive(false);
        }
    }
}
