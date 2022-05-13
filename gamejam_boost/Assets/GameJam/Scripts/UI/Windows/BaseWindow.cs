using GameJam.Scripts.Systems;
using UnityEngine;

namespace GameJam.Scripts.UI.Windows
{
    public abstract class BaseWindow : MonoBehaviour
    {
        protected Game Game;
        
        public virtual void Setup(Game game)
        {
            Game = game;
        }
        
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
        public virtual void Setup(Game game, T model)
        {
            Game = game;
            Model = model;
        }
    }
}