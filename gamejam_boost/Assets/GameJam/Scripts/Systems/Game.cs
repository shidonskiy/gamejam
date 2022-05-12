using System.Linq;
using GameJam.Scripts.Models;
using GameJam.Scripts.UI.Windows;
using UnityEngine;

namespace GameJam.Scripts.Systems
{
    public class Game : MonoBehaviour
    {
        [SerializeField] private WindowManager _windowManager;
        [SerializeField] private LevelManager _levelManager;

        public WindowManager WindowManager => _windowManager;
        public LevelManager LevelManager => _levelManager;

        private void Start()
        {
            MainscreenWindow window = WindowManager.OpenWindow<MainscreenWindow>();
            MainscreenModel model = new MainscreenModel();
            model.Levels = _levelManager.LevelsData.Levels.Select(l => new LevelModel(l.LevelName, l.LevelBuildId))
                .ToList();
            window.Setup(this, model);
        }
    }
}