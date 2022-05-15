using GameJam.Scripts.Obstacles;
using UnityEngine;

namespace GameJam.Scripts.UI.Elements
{
    public class PointsController : MonoBehaviour
    {
        [SerializeField] private PointsState _goodPoints;
        [SerializeField] private PointsState _badPoints;

        public void UpdatePoints(BaseObstacle.ObstacleState state, int count)
        {
            if (state == BaseObstacle.ObstacleState.Good)
            {
                _goodPoints.gameObject.SetActive(true);
                _badPoints.gameObject.SetActive(false);
                _goodPoints.UpdateCount(count);
            }
            else
            {
                _goodPoints.gameObject.SetActive(false);
                _badPoints.gameObject.SetActive(true);
                _badPoints.UpdateCount(count);
            }
        }
    }
}