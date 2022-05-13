using System;
using GameJam.Scripts.Obstacles.States;

namespace GameJam.Scripts.Obstacles
{
    public class Point : BaseObstacle<PointState>
    {
        private void Awake()
        {
            GoodState.PointConsumed += GoodStateOnPointConsumed;
            BadState.PointConsumed += GoodStateOnPointConsumed;
        }

        private void OnDestroy()
        {
            GoodState.PointConsumed -= GoodStateOnPointConsumed;
            BadState.PointConsumed -= GoodStateOnPointConsumed;
        }

        private void GoodStateOnPointConsumed()
        {
            gameObject.SetActive(false);
        }
    }
}