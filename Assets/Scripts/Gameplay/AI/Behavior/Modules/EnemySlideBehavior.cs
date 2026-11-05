using System;
using UnityEngine;
using static Gameplay.Player.PlayerController;

namespace Gameplay.AI.Behavior.Modules
{
    [Serializable]
    public sealed class EnemySlideBehavior : EnemyBehaviorModule
    {
        [SerializeField, Min(0f)] float duration;

        internal bool canSlide;
        internal bool isSliding;
        
        bool _hasSlid;
        float _elapsedSlideTime;
        Vector2 _slideStart;
        Vector2 _slideTarget;

        protected internal override void Update()
        {
            if (!enabled) return;
            if (controller.hit) canSlide = true;
            if (!canSlide) return;
            if (!isSliding) BeginSlide();
            if (isSliding) UpdatePosition();
        }

        void BeginSlide()
        {
            if (_hasSlid) return;

            controller.movement.enabled = false;
            _hasSlid = true;
            isSliding = true;
            _elapsedSlideTime = 0f;
            controller.rb.linearVelocityX = 0f;
            controller.rb.transform.rotation = Quaternion.Euler(0, controller.rb.transform.rotation.y == 0 ? 180 : 0, 0) ;
            _slideStart = controller.rb.position;
            _slideTarget = new Vector2(2f * playerPosition.x - _slideStart.x, _slideStart.y);
        }

        void UpdatePosition()
        {
            _elapsedSlideTime += Time.deltaTime;

            float progress = duration <= Mathf.Epsilon ? 1f : _elapsedSlideTime / duration;
            float easedProgress = Mathf.SmoothStep(0f, 1f, progress);

            Vector2 position = controller.rb.position;
            position.x = Mathf.Lerp(_slideStart.x, _slideTarget.x, easedProgress);
            controller.rb.MovePosition(position);

            if (!(progress >= 1f)) return;
            
            enabled = isSliding = false;
            controller.movement.enabled = true;
        }
    }
}
