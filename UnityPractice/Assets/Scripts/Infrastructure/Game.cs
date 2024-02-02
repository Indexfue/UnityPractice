using UnityEngine;
using UnityPractice.Infrastructure.Services;
using UnityPractice.Logic;

namespace UnityPractice.Infrastructure
{
    public class Game
    {
        public GameStateMachine StateMachine;

        public Game(ICoroutineRunner coroutineRunner, LoadingCurtain loadingCurtain)
        {
            StateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), loadingCurtain, AllServices.Container);
        }
    }
}