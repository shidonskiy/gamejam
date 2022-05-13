using System;
using GameJam.Scripts.Models;
using GameJam.Scripts.Systems;
using UnityEngine;
using UnityEngine.UI;

namespace GameJam.Scripts.UI.Windows
{
    public class LevelWindow : BaseWindow
    {
        [SerializeField] private Button _menuButton;

        void Awake()
        {
            _menuButton.onClick.AddListener(OnMenuButtonClick);
        }

        protected virtual void OnMenuButtonClick()
        {
            Game.WindowManager.OpenMainmenu();
        }
    }
}