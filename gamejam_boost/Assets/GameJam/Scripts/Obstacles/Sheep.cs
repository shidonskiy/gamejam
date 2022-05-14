using GameJam.Scripts.Obstacles.States;

namespace GameJam.Scripts.Obstacles
{
    public class Sheep : BaseObstacle<SheepState>
    {
        protected override bool SyncPosition => true;
        
        private void Awake()
        {
            var position = transform.position;
            GoodState.Setup(position);
            BadState.Setup(position);
        }
    }
}