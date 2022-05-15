using GameJam.Scripts.Levels;
using GameJam.Scripts.Systems;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameJam.Scripts.UI.Windows
{
    public class RestartWindow : BaseWindow
    {
        void Update()
        {
            if (Input.anyKeyDown)
            {
                Game.LevelManager.RestartLevel();
            }
        }
    }
}