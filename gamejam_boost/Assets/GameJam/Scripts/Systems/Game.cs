using GameJam.Scripts.UI.Windows;
using UnityEngine;

namespace GameJam.Scripts.Systems
{
    public class Game : MonoBehaviour
    {
        [SerializeField] private WindowManager _windowManager;

        public WindowManager WindowManager => _windowManager;

        private void Start()
        {
            WindowManager.OpenWindow<MainscreenWindow>();
        }
    }
}