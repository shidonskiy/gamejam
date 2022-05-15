using GameJam.Scripts.Levels;
using GameJam.Scripts.Systems;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameJam.Scripts.UI.Windows
{
    public class LevelWindow : BaseWindow<Level>
    {
        [SerializeField] private Button _menuButton;
        [SerializeField] private TextMeshProUGUI _pointsText;

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
        }

        private void ModelOnRestartLevel()
        {
            Game.WindowManager.OpenWindow<RestartWindow>(WindowManager.WindowMode.Clear).Setup(Game);
        }

        private void ModelOnPointsGained(int current, int max)
        {
            _pointsText.text = $"{current}/{max}";
        }

        protected virtual void OnMenuButtonClick()
        {
            Game.LevelManager.RestartLevel();
        }
    }
}