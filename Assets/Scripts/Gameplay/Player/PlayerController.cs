using UnityEngine;

namespace Gameplay.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] float jumpForce;
        
        [SerializeField] GameObject attack;
        [SerializeField] float attackSpeed;
        
        Rigidbody2D _rb;
        
        void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        void Update()
        {
        
        }

        public void Jump()
        {
            _rb.AddForce(transform.up * jumpForce,  ForceMode2D.Impulse);
        }

        public void Slash()
        {
            GameObject ins = Instantiate(attack, transform.position, Quaternion.identity);
            if (ins.TryGetComponent(out Rigidbody2D rb)){ rb.AddForceX(attackSpeed, ForceMode2D.Impulse); }
        }
        
        public void Defend()
        {
            Debug.Log("The player is defending");
        }
    }
}
