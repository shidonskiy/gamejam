using GameJam.Scripts.Levels;
using GameJam.Scripts.Obstacles;
using GameJam.Scripts.Systems;
using GameJam.Scripts.UI.Elements;
using UnityEngine;
using UnityEngine.UI;

namespace GameJam.Scripts.UI.Windows
{
    public class LevelWindow : BaseWindow<Level>
    {
        [SerializeField] private Button _menuButton;
        [SerializeField] private PointsController _points;

        void Awake()
        {
            _menuButton.onClick.AddListener(OnMenuButtonClick);
        }

        private void OnDestroy()
        {
            if(Model != null)
            {
                Model.PointsGained -= ModelOnPointsGained;
            }
        }

        public override void Setup(Game game, Level model)
        {
            base.Setup(game, model);
            
            Model.PointsGained += ModelOnPointsGained;
            Model.RestartLevel += ModelOnRestartLevel;
            Model.LevelCompleted += ModelOnLevelCompleted;
        }

        private void ModelOnLevelCompleted()
        {
            Game.WindowManager.OpenWindow<CompleteWindow>(WindowManager.WindowMode.Clear).Setup(Game);
        }

        private void ModelOnRestartLevel()
        {
            Game.WindowManager.OpenWindow<RestartWindow>(WindowManager.WindowMode.Clear).Setup(Game);
        }

        private void ModelOnPointsGained(BaseObstacle.ObstacleState state, int current)
        {
            _points.UpdatePoints(state, current);
        }

        protected virtual void OnMenuButtonClick()
        {
            Game.LevelManager.RestartLevel();
        }
    }
}