using System;
using GameJam.Scripts.Obstacles.States;
using UnityEngine;

namespace GameJam.Scripts.Obstacles
{
    public class MovingPlatform : BaseObstacle<MovingObjectState>
    {
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