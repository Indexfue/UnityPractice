using UnityEngine;

namespace UnityPractice.Infrastructure
{
    public class GameBootsrapper : MonoBehaviour
    {
        private Game _game;

        private void Awake()
        {
            _game = new Game();
            DontDestroyOnLoad(this);
        }
    }
}