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

        private BaseObstacle.ObstacleState _prevState;
        private List<BaseObstacle> _obstacles;

        private int _currentPoints = 0;
            
        public override void Setup(Game game)
        {
            base.Setup(game);

            LevelWindow window = Game.WindowManager.OpenWindow<LevelWindow>(WindowManager.WindowMode.Clear);
            window.Setup(Game);
        }

        public void Awake()
        {
            _obstacles = GetLevelObstacles();
            
            _player.PointCollected += PlayerOnPointCollected;
        }

        public void Start()
        {
            UpdateState(_obstacles);
        }

        private void OnValidate()
        {
            if (_prevState != _currentState)
            {
                UpdateState(GetLevelObstacles());
                
                _prevState = _currentState;
            }
        }
        
        private void PlayerOnPointCollected(PointState.PointType pointType)
        {
            _currentPoints++;
            Refresh();
        }

        private void Refresh()
        {
            if (_currentPoints >= _pointToSwitch)
            {
                _currentPoints = 0;
                _currentState = _currentState == BaseObstacle.ObstacleState.Bad
                    ? BaseObstacle.ObstacleState.Good
                    : BaseObstacle.ObstacleState.Bad;
                
                UpdateState(_obstacles);
            }
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
        }
    }
}