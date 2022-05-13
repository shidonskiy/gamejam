using System;
using System.Collections.Generic;
using GameJam.Scripts.Obstacles;
using GameJam.Scripts.Systems;
using GameJam.Scripts.UI.Windows;
using UnityEngine;

namespace GameJam.Scripts.Levels
{
    public class Level : BaseLevel
    {
        [SerializeField] private BaseObstacle.ObstacleState _currentState;
        [SerializeField] private List<Transform> _obstaclesRoot;

        private BaseObstacle.ObstacleState _prevState;
        private List<BaseObstacle> _obstacles;
            
        public override void Setup(Game game)
        {
            base.Setup(game);

            LevelWindow window = Game.WindowManager.OpenWindow<LevelWindow>(WindowManager.WindowMode.Clear);
            window.Setup(Game);
        }

        public void Awake()
        {
            _obstacles = GetLevelObstacles();
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