using GameJam.Scripts.Models;
using GameJam.Scripts.Systems;
using GameJam.Scripts.UI.Elements;
using UnityEngine;

namespace GameJam.Scripts.UI.Windows
{
    public class MainscreenWindow : BaseWindow<MainscreenModel>
    {
        [SerializeField] private Transform content;
        [SerializeField] private LevelElement levelPrefab;

        public override void Setup(Game game, MainscreenModel model)
        {
            base.Setup(game, model);
            
            foreach (var level in model.Levels)
            {
                LevelElement levelElement = Instantiate(levelPrefab, content);
                levelElement.Setup(level);
                
                levelElement.LevelSelected += LevelElementOnLevelSelected;
            }
        }

        private void LevelElementOnLevelSelected(int levelId)
        {
            Game.LevelManager.LoadLevel(levelId);
        }
    }
}