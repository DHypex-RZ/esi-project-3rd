using UnityEngine;

namespace Gameplay.AI.Modules
{
    [RequireComponent(typeof(SlidingModule), typeof(BackwardsModule))]
    public class ZigZagModule: AdvanceModuleController
    {
        SlidingModule _sliding;
        BackwardsModule _backwards;

        AdvanceModuleController _first;
        AdvanceModuleController _second;

        protected override void Awake()
        {
            base.Awake();
            
            _sliding = GetComponent<SlidingModule>();
            _backwards = GetComponent<BackwardsModule>();
        }
        
        protected void Start()
        {
            _first = Random.Range(0, 2) == 0 ? _sliding : _backwards;
            _second = _first == _sliding ? _backwards : _sliding;

            _second.enabled = false;
        }

        void Update()
        {
            if (!done) return;

            if (_first.done)
            {
                _second.enabled = true;
                _first.enabled = false;
            } 
            else if (_second.done)
            {
                _first.enabled = true;
                _second.enabled = false;
            }
        }

        void OnTriggerEnter2D(Collider2D other) { if (other.CompareTag("PlayerAttack") && controller.score.live) done = true; }
    }
}