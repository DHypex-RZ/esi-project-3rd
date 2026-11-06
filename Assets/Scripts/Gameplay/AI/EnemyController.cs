using Gameplay.AI.Modules;
using UnityEngine;

namespace Gameplay.AI
{
    [RequireComponent(typeof(MovementModule), typeof(AttackModule), typeof(ScoreModule))]
    public class EnemyController : MonoBehaviour
    {
        internal MovementModule movement;
        internal AttackModule attack;
        internal ScoreModule score;
        internal SpriteRenderer sprite;
        internal Animator animator;

        void Awake()
        {
            movement = GetComponent<MovementModule>();
            attack = GetComponent<AttackModule>();
            score = GetComponent<ScoreModule>();
            sprite = GetComponent<SpriteRenderer>();
            animator = GetComponent<Animator>();
        }
        
        internal void EnableModules(bool on = true) => movement.enabled = attack.enabled = score.enabled = on;
    }
}