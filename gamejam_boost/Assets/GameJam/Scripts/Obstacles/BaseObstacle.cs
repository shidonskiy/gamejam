using GameJam.Scripts.Obstacles.States;
using UnityEngine;

namespace GameJam.Scripts.Obstacles
{
    public abstract class BaseObstacle : MonoBehaviour
    {
        public enum ObstacleState
        {
            Good,
            Bad
        }
        
        [SerializeField] private ObstacleState _currentState;

        protected ObstacleState CurrentState => _currentState;
        
        private ObstacleState _prevState;

        private bool _isTransition;

        public void ChangeState(ObstacleState state)
        {
            ChangeStateInternal(state);
            _currentState = state;
        }

        public abstract void ShowAll();

        public abstract void UpdateTransition(Vector3 origin, float radius, ObstacleState newState);

        public abstract void ChangeStateInternal(ObstacleState state);

        private void OnValidate()
        {
            if (_prevState != _currentState)
            {
                ChangeState(_currentState);
                _prevState = _currentState;
            }
        }
    }
    
    public abstract class BaseObstacle<T> : BaseObstacle where T : ObjectState
    {
        [SerializeField] private T _goodState;
        [SerializeField] private T _badState;

        protected T GoodState => _goodState;
        protected T BadState => _badState;

        protected T CurrentObject;

        public override void UpdateTransition(Vector3 origin, float radius, ObstacleState newState)
        {
            if(CurrentState == newState)
            {
                return;
            }

            int overlapType = CheckOverlap(origin, radius, CurrentObject.GetBounds());
            if (overlapType == -1)
            {
                ChangeState(newState);
            }
            else if(overlapType == 0)
            {
                ShowAll();
            }
            
            GoodState.UpdateTransitionDirection(newState != ObstacleState.Good);
            BadState.UpdateTransitionDirection(newState != ObstacleState.Bad);
        }

        public override void ChangeStateInternal(ObstacleState state)
        {
            switch (state)
            {
                case ObstacleState.Good:
                    _goodState.Show();
                    _badState.Hide();
                    CurrentObject = _goodState;
                    break;
                case ObstacleState.Bad:
                    _goodState.Hide();
                    _badState.Show();
                    CurrentObject = _badState;
                    break;
            }
        }

        public override void ShowAll()
        {
            _goodState.Show();
            _badState.Show();
        }

        int CheckOverlap(Vector2 origin, float radius , Bounds bounds)
        {
            Vector3 nearest = bounds.ClosestPoint(origin);
            float dMin = Vector2.Distance(origin, bounds.min);
            float dMax = Vector2.Distance(origin, bounds.max);
            float dNear = Vector2.Distance(origin, nearest);
            
            
            if (bounds.Contains(new Vector3(origin.x, origin.y, bounds.center.z))
                && dMin > radius && dMax > radius)
            {
                return 0;
            }

            if (dNear < radius && (dMin > radius || dMax > radius))
            {
                return 0;
            }

            if (dMin > radius && dMax > radius)
            {
                return 1;
            }

            if(dMin < radius && dMax < radius)
            {
                return -1;
            }

            return 0;
        }
    }
}