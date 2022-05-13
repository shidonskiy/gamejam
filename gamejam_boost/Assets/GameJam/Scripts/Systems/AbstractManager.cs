using UnityEngine;

namespace GameJam.Scripts.Systems
{
    public class AbstractManager : MonoBehaviour
    {
        protected Game Game;
        
        public virtual void Setup(Game game)
        {
            Game = game;
        }
    }
}