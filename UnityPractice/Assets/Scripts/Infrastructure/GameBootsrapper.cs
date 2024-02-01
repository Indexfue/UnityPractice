using UnityPractice.Logic;
using UnityEngine;
using UnityPractice.Infrastructure.States;

namespace UnityPractice.Infrastructure
{
    public class GameBootsrapper : MonoBehaviour, ICoroutineRunner
    {
        [SerializeField] private LoadingCurtain loadingCurtain;

        private Game _game;

        private void Awake()
        {
            _game = new Game(this, loadingCurtain);
            _game.StateMachine.EnterIn<BootstrapState>();

            DontDestroyOnLoad(this);
        }
    }
}