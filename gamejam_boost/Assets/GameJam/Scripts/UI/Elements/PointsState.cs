using System.Collections.Generic;
using UnityEngine;

namespace GameJam.Scripts.UI.Elements
{
    public class PointsState : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _points;

        public void UpdateCount(int count)
        {
            for (int i = 0; i < _points.Count; i++)
            {
                _points[i].SetActive(i < count);
            }
        }
    }
}