using UnityEngine;

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