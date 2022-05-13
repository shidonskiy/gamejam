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
            
        }

        public void Start()
        {
            
        }

        private void OnValidate()
        {
            if (_prevState != _currentState)
            {
                List<BaseObstacle> obstacles = GetLevelObstacles();

                foreach (var obstacle in obstacles)
                {
                    obstacle.ChangeState(_currentState);
                }
                
                _prevState = _currentState;
            }
        }

        private List<BaseObstacle> GetLevelObstacles()
        {
            List<BaseObstacle> obstacles = new List<BaseObstacle>();
            foreach (var root in _obstaclesRoot)
            {
                obstacles.Add(root.GetComponentInChildren<BaseObstacle>());
            }

            return obstacles;
        }
    }
}