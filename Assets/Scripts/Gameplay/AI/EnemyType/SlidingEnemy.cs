using System.Collections;
using UI;
using UnityEngine;
using static Gameplay.AI.EnemyGeneratorController;

namespace Gameplay.AI.EnemyType
{
    public class SlidingEnemy : EnemyController
    {
        [Header("Slide")]
        [SerializeField, Min(0.01f)] float slideDuration = 0.35f;

        bool _hasSlide;
        bool _isSliding;
        bool _isMovementLocked;


        protected override void FixedUpdate()
        {
            if (!_isMovementLocked) base.FixedUpdate();
        }

        
        protected override void TakeDamage()
        {
            if (_isSliding) return;

            if (!_hasSlide)
            {
                _hasSlide = true;
                StartCoroutine(SlideToOppositeSide());
                return;
            }

            PlayableInterfaceController.AddScore(score);
            Destroy(gameObject);
        }

        IEnumerator SlideToOppositeSide()
        {
            _isSliding = true;
            _isMovementLocked = true;
            rb.linearVelocityX = 0f;

            Vector2 start = rb.position;
            Vector2 target = new(2f * playerPosition.x - start.x, start.y);
            float elapsed = 0f;

            while (elapsed < slideDuration)
            {
                yield return new WaitForFixedUpdate();
                elapsed += Time.fixedDeltaTime;

                float t = Mathf.SmoothStep(0f, 1f, elapsed / slideDuration);
                rb.MovePosition(new Vector2(Mathf.Lerp(start.x, target.x, t), rb.position.y));
            }

            _isMovementLocked = false;
            _isSliding = false;
        }
    }
}
