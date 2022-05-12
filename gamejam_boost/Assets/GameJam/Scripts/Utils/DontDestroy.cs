using UnityEngine;

namespace GameJam.Scripts.Utils
{
    public class DontDestroy : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(this);
        }
    }
}