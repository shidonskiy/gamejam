using System;
using GameJam.Scripts.Obstacles;
using GameJam.Scripts.Obstacles.States;
using GameJam.Scripts.Utils;
using UnityEngine;

namespace GameJam.Scripts.Controllers
{
    public class Player : BaseObstacle<PlayerState>
    {
        private static readonly int JumpKey = Animator.StringToHash("Jump");
        private static readonly int MoveKey = Animator.StringToHash("Move");
        private static readonly int FallKey = Animator.StringToHash("Fall");

        public event Action<PointState.PointType> PointCollected;

        public void Jump()
        {
            if (GoodState.isActiveAndEnabled)
            {
                GoodState.Animator.SetTrigger(JumpKey);
            }
            if (BadState.isActiveAndEnabled)
            {
                BadState.Animator.SetTrigger(JumpKey);
            }
        }
        
        public void Move(float value)
        {
            if (GoodState.isActiveAndEnabled)
            {
                GoodState.Animator.SetFloat(MoveKey, value);
            }
            if (BadState.isActiveAndEnabled)
            {
                BadState.Animator.SetFloat(MoveKey, value);
            }
        }

        public void Fall(bool value)
        {
            if (GoodState.isActiveAndEnabled)
            {
                GoodState.Animator.SetBool(FallKey, value);
            }
            if (BadState.isActiveAndEnabled)
            {
                BadState.Animator.SetBool(FallKey, value);
            }
        }
        
        public void UpdateDirection(float direction)
        {
            GoodState.UpdateDirection(direction);
            BadState.UpdateDirection(direction);
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.layer == Layers.Point)
            {
                if (col.TryGetComponent(out PointState pointState))
                {
                    pointState.Consume();
                    OnPointCollected(pointState.Type);
                }
            }
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.collider.gameObject.layer == Layers.Spikes)
            {
                if (col.collider.TryGetComponent(out SpikeObjectState _))
                {
                    
                }
            }
        }

        protected virtual void OnPointCollected(PointState.PointType type)
        {
            PointCollected?.Invoke(type);
        }
    }
}