using UnityEngine;

namespace GameJam.Scripts.Obstacles.States
{
    public class PlayerState : ObjectState
    {
        [SerializeField] private Animator _animator;

        public Animator Animator => _animator;
        
        public void UpdateDirection(float direction)
        {
            if(Renderer != null)
            {
                Renderer.flipX = direction < 0;
            }
        }
    }
}