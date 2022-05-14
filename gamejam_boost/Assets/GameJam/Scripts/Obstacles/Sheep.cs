using GameJam.Scripts.Obstacles.States;

namespace GameJam.Scripts.Obstacles
{
    public class Sheep : BaseObstacle<SheepState>
    {
        private void Awake()
        {
            var position = transform.position;
            GoodState.Setup(position);
            BadState.Setup(position);
        }
        
        public override void ChangeStateInternal(ObstacleState state)
        {
            switch (state)
            {
                case ObstacleState.Good:
                    GoodState.transform.position = BadState.transform.position;
                    break;
                case ObstacleState.Bad:
                    BadState.transform.position = GoodState.transform.position;
                    break;
            }

            base.ChangeStateInternal(state);
        }
    }
}