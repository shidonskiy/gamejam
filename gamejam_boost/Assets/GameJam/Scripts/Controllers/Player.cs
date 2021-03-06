using System;
using System.Collections;
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
        public event Action Death;
        public event Action LevelFinished;

        public void Jump(bool value)
        {
            if (GoodState.isActiveAndEnabled)
            {
                GoodState.Animator.SetBool(JumpKey, value);
            }
            if (BadState.isActiveAndEnabled)
            {
                BadState.Animator.SetBool(JumpKey, value);
            }

            if (value)
            {
                StartCoroutine(_ResetJump());
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

        private IEnumerator _ResetJump()
        {
            yield return null;
            
            Jump(false);
        }

        public void Die()
        {
            OnDeath();
        }
        
        public void Complete()
        {
            OnLevelFinished();
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

        protected virtual void OnDeath()
        {
            Death?.Invoke();
        }
        
        protected virtual void OnLevelFinished()
        {
            LevelFinished?.Invoke();
        }
    }
}