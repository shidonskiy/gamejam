using GameJam.Scripts.Models;
using GameJam.Scripts.UI.Elements;
using UnityEngine;

namespace GameJam.Scripts.UI.Windows
{
    public class MainscreenWindow : BaseWindow<MainscreenModel>
    {
        [SerializeField] private Transform content;
        [SerializeField] private LevelElement levelPrefab;

        public override void Setup(MainscreenModel model)
        {
            foreach (var level in model.Levels)
            {
                LevelElement levelElement = Instantiate(levelPrefab, content);
                levelElement.Setup(level);
            }
        }
    }
}