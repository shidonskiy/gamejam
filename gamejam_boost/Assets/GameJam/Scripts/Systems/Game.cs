using System;
using System.Linq;
using GameJam.Scripts.Levels;
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

        private void Awake()
        {
            WindowManager.Setup(this);
            LevelManager.Setup(this);
            
            EventSink.LoadLevelFinish += EventSinkOnLoadLevelFinish;
        }

        private void OnDestroy()
        {
            EventSink.LoadLevelFinish -= EventSinkOnLoadLevelFinish;
        }

        private void EventSinkOnLoadLevelFinish(Level level)
        {
            level.Setup(this);
        }

        private void Start()
        {
            WindowManager.OpenMainmenu();
        }
    }
}