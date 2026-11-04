using UnityEngine;
using static Gameplay.AI.EnemyController;

namespace Gameplay.AI.Behavior.Modules
{
    [System.Serializable]
    public sealed class EnemySlideBehavior : EnemyBehaviorModule
    {
        [Header("Slide")]
        [SerializeField, Min(0.01f)] float slideDuration = 0.35f;

        bool _hasSlid;
        bool _isSliding;
        float _slideDirectionX;
        float _elapsedSlideTime;
        Vector2 _slideStart;
        Vector2 _slideTarget;

        internal override bool OnFixedUpdate()
        {
            if (!_isSliding) return false;

            _elapsedSlideTime += Time.fixedDeltaTime;
            MoveTowards(_slideStart, _slideTarget, _elapsedSlideTime);
            if (_elapsedSlideTime >= slideDuration) _isSliding = false;

            return true;
        }

        internal override bool OnEnemyDamaged()
        {
            if (!enabled) return false;
            if (player == null) return false;
            if (_isSliding) return true;
            if (_hasSlid) return false;

            _hasSlid = true;
            BeginSlide();
            return true;
        }

        internal override void OnTriggerEnter2D(Collider2D other)
        {
            if (!enabled || !_isSliding) return;

            EnemyController enemy = other.GetComponentInParent<EnemyController>();
            if (enemy != null && enemy != controller) PushEnemy(enemy);
        }

        void BeginSlide()
        {
            _isSliding = true;
            _elapsedSlideTime = 0f;
            controller.BeginPushSequence();
            controller.rb.linearVelocityX = 0f;

            _slideStart = controller.rb.position;
            _slideTarget = GetOppositeSide(_slideStart);
            _slideDirectionX = Mathf.Sign(_slideTarget.x - _slideStart.x);
        }

        Vector2 GetOppositeSide(Vector2 start)
        {
            return new Vector2(2f * playerPosition.x - start.x, start.y);
        }

        void MoveTowards(Vector2 start, Vector2 target, float elapsed)
        {
            float t = Mathf.SmoothStep(0f, 1f, elapsed / slideDuration);
            Vector2 targetPosition = new(Mathf.Lerp(start.x, target.x, t), controller.rb.position.y);
            EnemyController blockingEnemy = controller.MoveSafely(targetPosition);
            if (blockingEnemy is not null) PushEnemy(blockingEnemy);
        }

        void PushEnemy(EnemyController enemy)
        {
            controller.Push(enemy, Vector2.right * _slideDirectionX);
        }
    }
}
