using UnityEngine;

namespace GameJam.Scripts.Obstacles.States
{
    public class PlayerState : ObjectState
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private SpriteRenderer _spriteRenderer;

        public Animator Animator => _animator;
        
        public void UpdateDirection(float direction)
        {
            _spriteRenderer.flipX = direction < 0;
        }
    }
}