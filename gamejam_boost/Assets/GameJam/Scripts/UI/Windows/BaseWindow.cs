using UnityEngine;

namespace GameJam.Scripts.UI.Windows
{
    public abstract class BaseWindow : MonoBehaviour
    {
        public virtual void Show()
        {
            gameObject.SetActive(true);
        }

        public virtual void Hide()
        {
            gameObject.SetActive(false);
        }
    }
    
    public abstract class BaseWindow<T> : BaseWindow
    {
        public abstract void Setup(T model);
    }
}