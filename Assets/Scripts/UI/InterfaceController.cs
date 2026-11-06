using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class InterfaceController : MonoBehaviour
    {
        public static InterfaceController canvas { get; private set; }
        
        [SerializeField] Text scoreText;
        [SerializeField] Image healthSprite;
        
        void Awake() => canvas = this;

        internal void UpdateScore(int score) => scoreText.text = score.ToString();
        
        internal void UpdateHealth(float health, float currentHealth) => healthSprite.fillAmount = currentHealth / health;
    }
}
