using UnityEngine;

namespace GameJam.Scripts.Obstacles.States
{
    public class BadSheepState : SheepState
    {
        [SerializeField] private float _speed;
        [SerializeField] private AnimationCurve _curve;
        [SerializeField] private float _waitTime = 0.5f;

        private float _currentTime;
        private bool _isWaiting;

        private void FixedUpdate()
        {
            if (!_isInited)
            {
                return;
            }
            
            if(_isWaiting)
            {
                _currentTime += Time.fixedDeltaTime;

                if (_currentTime > _waitTime)
                {
                    _currentTime = 0;
                    _isWaiting = false;
                }
                return;
            }

            var step = _speed * Time.fixedDeltaTime;
            var position = transform.position;
            float t = Vector2.Distance(position, _currentTarget) / _distance;
            step *= _curve.Evaluate(t);
            _rb.MovePosition(Vector3.MoveTowards(position, _currentTarget, step));

            if (Vector2.Distance(_rb.position, _currentTarget) <= Mathf.Epsilon)
            {
                _isWaiting = true;
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