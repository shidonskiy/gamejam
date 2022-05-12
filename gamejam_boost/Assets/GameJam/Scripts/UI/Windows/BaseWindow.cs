using GameJam.Scripts.Systems;
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
        protected T Model;
        protected Game Game;
        public virtual void Setup(Game game, T model)
        {
            Game = game;
            Model = model;
        }
    }
}