using System;
using GameJam.Scripts.Obstacles.States;
using UnityEngine;

namespace GameJam.Scripts.Obstacles
{
    public class MovingPlatform : BaseObstacle<MovingObjectState>
    {
        protected override bool SyncPosition => true;
    }
}