using UnityEngine;

namespace GameJam.Scripts.Obstacles.States
{
    public class GoodSheepState : SheepState
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _bounceForce;

        public float BounceForce => _bounceForce;

        private void FixedUpdate()
        {
            if (!_isInited)
            {
                return;
            }
            
            var step = _speed * Time.fixedDeltaTime;
            _rb.MovePosition(Vector2.MoveTowards(transform.position, _currentTarget, step));

            if (Vector2.Distance(_rb.position, _currentTarget) <= Mathf.Epsilon)
            {
                _inverseDir = !_inverseDir;
                _currentTarget = _inverseDir ? _originalTarget : _targetPosition.position;
            }
        }

        private void OnDrawGizmosSelected()
        {
            Vector3 origin = transform.TransformPoint(Vector3.zero);
            Gizmos.DrawLine(origin, _targetPosition.transform.position);
        }
    }
}