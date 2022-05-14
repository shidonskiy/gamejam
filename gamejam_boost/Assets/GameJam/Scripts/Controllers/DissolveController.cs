using System;
using GameJam.Scripts.Levels;
using UnityEngine;

namespace GameJam.Scripts.Controllers
{
    public class DissolveController : MonoBehaviour
    {
        [SerializeField] private float _radius;
        [SerializeField] private Level _level;
        [SerializeField] private float _lineWidth;

        public void StartTransition()
        {
            Shader.SetGlobalFloat("_DissolveEnabled", 1);
        }

        public void FinishTransition()
        {
            Shader.SetGlobalFloat("_DissolveEnabled", 0);
        }
        
        public void UpdateTransition(Vector3 origin, float radius, float maxRadius)
        {
            Shader.SetGlobalFloat("_DissolveRadius", radius);
            Shader.SetGlobalVector("_DissolvePosition", origin);
            Shader.SetGlobalFloat("_DissolveLineWidth", _lineWidth - _lineWidth * (radius / maxRadius));
        }

        private void OnValidate()
        {
            StartTransition();
            UpdateTransition(Vector3.zero, _radius, _level.MaxRadius);
            _level.ShowAll();
        }
    }
}