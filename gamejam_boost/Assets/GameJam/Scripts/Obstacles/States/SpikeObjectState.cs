using System;
using System.Collections;
using UnityEngine.Events;

namespace GameJam.Scripts.Obstacles.States
{
    public class SpikeObjectState : ObjectState
    {
        public void Touch()
        {
            LevelRestart.ReloadScene();
        }
    }
}