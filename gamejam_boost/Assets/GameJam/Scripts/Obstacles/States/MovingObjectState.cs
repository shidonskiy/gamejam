using System;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace GameJam.Scripts.Obstacles.States
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class MovingObjectState : ObjectState
    {
        public float speed;

        [SerializeField] private Transform _targetPosition;

        private Rigidbody2D _rb;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            var step = speed * Time.fixedDeltaTime;
            _rb.MovePosition(Vector3.MoveTowards(transform.position, _targetPosition.position, step));
        }
    }
}