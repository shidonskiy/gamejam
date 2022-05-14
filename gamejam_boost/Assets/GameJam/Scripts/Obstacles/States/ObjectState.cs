using UnityEngine;

namespace GameJam.Scripts.Obstacles.States
{
    public class ObjectState : MonoBehaviour
    {
        private Collider2D _collider;
        private Renderer _renderer;
        private MaterialPropertyBlock _block;
        
        protected virtual void Awake()
        {
            _collider = GetComponent<Collider2D>();
            _renderer = GetComponent<Renderer>();
            _block = new MaterialPropertyBlock();
        }
        
        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void UpdateTransitionDirection(bool inverse)
        {
            if (_renderer != null)
            {
                _renderer.GetPropertyBlock(_block);
                _block.SetFloat("_DissolveInverse", inverse ? 1 : 0);
                _renderer.SetPropertyBlock(_block);
            }
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public virtual Bounds GetBounds()
        {
            Bounds bounds = new Bounds(transform.position, Vector3.zero);
            
            if (_collider != null)
            {
                bounds = _collider.bounds;
            }

            return bounds;
        }
    }
}