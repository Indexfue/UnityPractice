using System;
using UnityEngine;

namespace UnityPractice.Infrastructure.States
{
    public class BootstrapState : IState
    {
        private readonly string Initial = "Initial";

        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;

        public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
        }

        public void Enter()
        {
            RegisterServices();
            EnterLoadLevel();
        }

        public void Exit()
        {
            
        }

        private void RegisterServices()
        {
            Game.InputService = RegisterInputService();
        }
         
        private static IInputService RegisterInputService()
        {
            if (Application.isEditor)
                return new StandaloneInputService();
            else
                return new MobileInputService();
        }

        private void EnterLoadLevel() => _stateMachine.EnterIn<LoadLevelState, string>("Main");
    }
}