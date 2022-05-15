using System;
using System.Collections;
using System.Collections.Generic;
using GameJam.Scripts.Controllers;
using GameJam.Scripts.Obstacles;
using GameJam.Scripts.Obstacles.States;
using GameJam.Scripts.Systems;
using GameJam.Scripts.UI.Windows;
using UnityEngine;

namespace GameJam.Scripts.Levels
{
    public class Level : BaseLevel
    {
        [SerializeField] private BaseObstacle.ObstacleState _currentState;
        [SerializeField] private List<Transform> _obstaclesRoot;
        [SerializeField] private int _pointToSwitch = 3;
        [SerializeField] private Player _player;

        [SerializeField] private DissolveController _controller;
        [SerializeField] private float _maxRadius;
        [SerializeField] private float _transitionTime;
        [SerializeField] private float _radius;
        [SerializeField] private bool _isTransition;

        [SerializeField] private CrossfadeAudio _audio;

        public float MaxRadius => _maxRadius;
        
        private BaseObstacle.ObstacleState _prevState;
        private List<BaseObstacle> _obstacles;

        private int _currentPoints = 0;
        private Coroutine _transitionRoutine;

        public event Action<BaseObstacle.ObstacleState, int> PointsGained;
        public event Action RestartLevel;
        public event Action LevelCompleted;

        public override void Setup(Game game)
        {
            base.Setup(game);

            LevelWindow window = Game.WindowManager.OpenWindow<LevelWindow>(WindowManager.WindowMode.Clear);
            window.Setup(Game, this);
            OnPointsGained(_currentPoints);
        }

        public void ShowAll()
        {
            var obstacles = GetLevelObstacles();

            foreach (var obstacle in obstacles)
            {
                obstacle.ShowAll();
            }
        }

        private void Awake()
        {
            _obstacles = GetLevelObstacles();
            
            _player.PointCollected += PlayerOnPointCollected;
            _player.Death += PlayerOnDeath;
            _player.LevelFinished += PlayerOnLevelFinished;
            _controller.FinishTransition();
        }

        private void PlayerOnLevelFinished()
        {
            LevelComplete();
        }

        private void PlayerOnDeath()
        {
            Restart();
        }

        private void Start()
        {
            UpdateState(_obstacles);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                _currentPoints += 3;
                Refresh();
            }
        }

        private void OnValidate()
        {
            if (_prevState != _currentState)
            {
                UpdateState(GetLevelObstacles());
                _controller.UpdateState(_currentState);
                _prevState = _currentState;
            }
        }

        private void PlayerOnPointCollected(PointState.PointType pointType)
        {
            _currentPoints++;
            Refresh();
            
            OnPointsGained(_currentPoints);
        }

        private void Refresh()
        {
            if (_currentPoints >= _pointToSwitch)
            {
                _currentPoints = 0;
                BaseObstacle.ObstacleState prevState;
                prevState = _currentState;
                _currentState = _currentState == BaseObstacle.ObstacleState.Bad
                    ? BaseObstacle.ObstacleState.Good
                    : BaseObstacle.ObstacleState.Bad;

                if (_transitionRoutine != null)
                {
                    StopCoroutine(_transitionRoutine);
                    UpdateTransition(prevState, true);
                    _controller.FinishTransition();
                }
                _transitionRoutine = StartCoroutine(TransitionRoutine());
            }
        }

        private void UpdateTransition(BaseObstacle.ObstacleState state, bool last = false)
        {
            _controller.UpdateTransition(_player.transform.position, _radius, _maxRadius);
            
            foreach (var obstacle in _obstacles)
            {
                obstacle.UpdateTransition(_player.transform.position, _radius, state, last);
            }
        }

        private IEnumerator TransitionRoutine()
        {
            _isTransition = true;
            _audio.Fade(_currentState);
            float speed = _maxRadius / _transitionTime;
            float currentTime = 0;
            _radius = 0;
            
            _controller.StartTransition();
            _controller.UpdateState(_currentState);
            var state = _currentState;
            
            while (currentTime < _transitionTime)
            {
                _radius += speed * Time.deltaTime;
                UpdateTransition(state);
                yield return null;
                
                currentTime += Time.deltaTime;
            }

            _radius = _maxRadius;
            _isTransition = false;
            UpdateTransition(state,true);
            _controller.FinishTransition();

            _transitionRoutine = null;
        }

        private List<BaseObstacle> GetLevelObstacles()
        {
            List<BaseObstacle> obstacles = new List<BaseObstacle>();
            foreach (var root in _obstaclesRoot)
            {
                obstacles.AddRange(root.GetComponentsInChildren<BaseObstacle>(true));
            }

            return obstacles;
        }

        private void UpdateState(List<BaseObstacle> obstacles)
        {
            foreach (var obstacle in obstacles)
            {
                obstacle.ChangeState(_currentState);
            }
            _controller.UpdateState(_currentState);
            _controller.FinishTransition();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(_player.transform.position, _maxRadius);
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(_player.transform.position, _radius);
        }

        protected virtual void OnPointsGained(int current)
        {
            PointsGained?.Invoke(_currentState, current);
        }

        public void Restart()
        {
            RestartLevel?.Invoke();
        }
        
        public void LevelComplete()
        {
            OnLevelCompleted();
        }

        protected virtual void OnLevelCompleted()
        {
            LevelCompleted?.Invoke();
        }
    }
}