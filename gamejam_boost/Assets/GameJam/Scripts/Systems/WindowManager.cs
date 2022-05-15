using System;
using System.Collections.Generic;
using System.Linq;
using GameJam.Scripts.Models;
using GameJam.Scripts.UI.Windows;
using UnityEngine;

namespace GameJam.Scripts.Systems
{
    public class WindowManager : AbstractManager
    {
        [SerializeField] private Transform _windowsRoot;
        [SerializeField] private List<BaseWindow> _windows;

        private Dictionary<string, BaseWindow> _windowsMap;
        private Stack<BaseWindow> _windowsStack = new Stack<BaseWindow>();

        void Awake()
        {
            GenerateMap();
        }

        public T OpenWindow<T>(WindowMode mode = WindowMode.Replace) where T : BaseWindow
        {
            string key = typeof(T).Name;
            if(!_windowsMap.TryGetValue(key, out BaseWindow windowPrefab))
            {
                return null;
            }
            
            T window = Instantiate(windowPrefab, _windowsRoot) as T;

            switch (mode)
            {
                case WindowMode.Clear:
                    Clear();
                    break;
                case WindowMode.Add:
                    BaseWindow last = GetLast();
                    if(last != null)
                    {
                        last.Hide();
                    }
                    break;
                case WindowMode.Replace:
                    RemoveLast();
                    break;
            }
            
            _windowsStack.Push(window);

            return window;
        }

        public void OpenMainmenu(WindowMode mode = WindowMode.Add)
        {
            MainscreenWindow window = OpenWindow<MainscreenWindow>(mode);
            MainscreenModel model = new MainscreenModel
            {
                Levels = Game.LevelManager.LevelsData.Levels.Select(l => new LevelModel(l.LevelName, l.LevelBuildId))
                    .ToList()
            };
            window.Setup(Game, model);
        }

        public void GoBack()
        {
            if(_windowsStack.Count < 2)
            {
                return;
            }

            RemoveLast();
            BaseWindow last = GetLast();
            
            if(last != null)
            {
                last.Show();
            }
        }

        private void GenerateMap()
        {
            _windowsMap = new Dictionary<string, BaseWindow>();
            foreach (BaseWindow window in _windows)
            {
                _windowsMap[window.GetType().Name] = window;
            }
        }

        private void Clear()
        {
            while (_windowsStack.Count > 0)
            {
                RemoveLast();
            }
        }

        private void RemoveLast()
        {
            if (_windowsStack.Count > 0)
            {
                BaseWindow prevWindow = _windowsStack.Pop();
                Destroy(prevWindow.gameObject);
            }
        }

        private BaseWindow GetLast()
        {
            if (_windowsStack.Count > 0)
            {
                return _windowsStack.Peek();
            }

            return null;
        }
        
        public enum WindowMode
        {
            Clear,
            Add,
            Replace
        }
    }
}