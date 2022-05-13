using GameJam.Scripts.Systems;
using UnityEngine;

namespace GameJam.Scripts.Levels
{
    public abstract class BaseLevel : MonoBehaviour
    {
        protected Game Game;

        public virtual void Setup(Game game)
        {
            Game = game;
        }
    }
}