using UnityEngine;

namespace GameJam.Scripts.Obstacles.States
{
    public class SheepState : ObjectState
    {
        [SerializeField] protected Transform _targetPosition;

        protected Rigidbody2D _rb;
        protected Vector2 _currentTarget;
        protected Vector3 _originalTarget;
        protected bool _inverseDir;
        protected float _distance;

        protected bool _isInited;

        protected override void Awake()
        {
            base.Awake();
            _rb = GetComponent<Rigidbody2D>();
            _currentTarget = _targetPosition.position;
        }

        protected virtual void Update()
        {
            Renderer.flipX = _inverseDir;
        }

        public void Setup(Vector3 originTarget)
        {
            _isInited = true;
            _originalTarget = originTarget;
            _distance = Vector2.Distance(_originalTarget, _currentTarget);
        }
    }
}