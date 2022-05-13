using System;
using UnityEngine;

namespace GameJam.Scripts.Obstacles.States
{
    public class PointState : ObjectState
    {
        public enum PointType
        {
            Good,
            Bad
        }

        [SerializeField] private PointType _pointType;

        public PointType Type => _pointType;
        public event Action PointConsumed;

        public void Consume()
        {
            OnPointConsumed();
        }
        
        protected virtual void OnPointConsumed()
        {
            PointConsumed?.Invoke();
        }
    }
}