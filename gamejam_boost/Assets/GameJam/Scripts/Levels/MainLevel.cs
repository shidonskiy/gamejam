using GameJam.Scripts.Systems;

namespace GameJam.Scripts.Levels
{
    public class MainLevel : BaseLevel
    {
        public override void Setup(Game game)
        {
            base.Setup(game);
            Game.WindowManager.OpenMainmenu();
        }
    }
}