using System.Collections;
using UI;
using UnityEngine;
using static Gameplay.AI.EnemyGeneratorController;

namespace Gameplay.AI.EnemyType
{
    public class SlidingEnemy : EnemyController
    {
        [Header("Slide")]
        [SerializeField, Min(0.01f)] float slideDuration;

        [Header("Push")]
        [SerializeField, Min(0.01f)] float pushForce;
        [SerializeField, Min(0.01f)] float pushDuration;

        bool _hasSlide;
        bool _isSliding;
        float _slideDirectionX;

        protected override void FixedUpdate()
        {
            if (!_isSliding) base.FixedUpdate();
        }

        protected override void OnTriggerEnter2D(Collider2D other)
        {
            base.OnTriggerEnter2D(other);

            if (!_isSliding) return;

            EnemyController enemy = other.GetComponent<EnemyController>();
            if (enemy != null && enemy != this) PushEnemy(enemy);
        }

        void PushEnemy(EnemyController enemy)
        {
            enemy.Push(Vector2.right * _slideDirectionX, pushForce, pushDuration);
        }

        protected override void TakeDamage()
        {
            if (_isSliding) return;

            if (!_hasSlide) StarSlide();
            else Defeat();
        }

        void StarSlide()
        {
            _hasSlide = true;
            StartCoroutine(SlideToOppositeSide());
        }

        void Defeat()
        {
            PlayableInterfaceController.AddScore(score);
            Destroy(gameObject);
        }

        IEnumerator SlideToOppositeSide()
        {
            _isSliding = true;
            rb.linearVelocityX = 0f;

            Vector2 start = rb.position;
            Vector2 target = GetOppositeSide(start);
            _slideDirectionX = Mathf.Sign(target.x - start.x);
            float elapsed = 0f;

            while (elapsed < slideDuration)
            {
                yield return new WaitForFixedUpdate();
                elapsed += Time.fixedDeltaTime;
                MoveTowards(start, target, elapsed);
            }

            _isSliding = false;
        }

        Vector2 GetOppositeSide(Vector2 start)
        {
            return new Vector2(2f * playerPosition.x - start.x, start.y);
        }

        void MoveTowards(Vector2 start, Vector2 target, float elapsed)
        {
            float t = Mathf.SmoothStep(0f, 1f, elapsed / slideDuration);
            rb.MovePosition(new Vector2(Mathf.Lerp(start.x, target.x, t), rb.position.y));
        }
    }
}
