using GameJam.Scripts.Systems;
using GameJam.Scripts.UI.Windows;
using UnityEngine;

namespace GameJam.Scripts.Levels
{
    public class Level : MonoBehaviour
    {
        private Game _game;

        public void Setup(Game game)
        {
            _game = game;

            LevelWindow window = _game.WindowManager.OpenWindow<LevelWindow>(WindowManager.WindowMode.Clear);
            window.Setup(_game);
        }
    }
}