using UnityEngine;

namespace GameJam.Scripts.Obstacles.States
{
    public class ObjectState : MonoBehaviour
    {
        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}