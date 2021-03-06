using GameJam.Scripts.Systems;
using UnityEngine;

namespace GameJam.Scripts.UI.Windows
{
    public class CompleteWindow : BaseWindow
    {
        void Update()
        {
            if (Input.anyKeyDown)
            {
                Game.WindowManager.OpenMainmenu(WindowManager.WindowMode.Clear);
            }
        }
    }
}