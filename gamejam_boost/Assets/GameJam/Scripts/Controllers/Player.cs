using System;
using GameJam.Scripts.Obstacles.States;
using GameJam.Scripts.Utils;
using UnityEngine;

namespace GameJam.Scripts.Controllers
{
    public class Player : MonoBehaviour
    {
        public event Action<PointState.PointType> PointCollected; 

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