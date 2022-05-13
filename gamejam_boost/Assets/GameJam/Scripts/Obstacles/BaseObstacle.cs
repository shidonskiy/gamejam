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

        public void ChangeState(ObstacleState state)
        {
            ChangeStateInternal(state);
            _currentState = state;
        }

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
        
        public override void ChangeStateInternal(ObstacleState state)
        {
            switch (state)
            {
                case ObstacleState.Good:
                    _goodState.Show();
                    _badState.Hide();
                    break;
                case ObstacleState.Bad:
                    _goodState.Hide();
                    _badState.Show();
                    break;
            }
        }
    }
}