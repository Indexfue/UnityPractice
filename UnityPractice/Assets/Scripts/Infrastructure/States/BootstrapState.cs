using System;
using UnityEngine;
using UnityPractice.Infrastructure.AssetManagement;
using UnityPractice.Infrastructure.Factories;
using UnityPractice.Infrastructure.Services;

namespace UnityPractice.Infrastructure.States
{
    public class BootstrapState : IState
    {
        private readonly string Initial = "Initial";

        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly AllServices _services;

        public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader, AllServices services)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _services = services;
            
            RegisterServices();
        }

        public void Enter()
        {
            _stateMachine.EnterIn<LoadLevelState, string>("Main");
        }

        public void Exit()
        {
            
        }

        private void RegisterServices()
        {
            _services.RegisterSingle<IInputService>(InputService());
            _services.RegisterSingle<IAssets>(new AssetsProvider());
            _services.RegisterSingle<IGameFactory>(new GameFactory(_services.Single<IAssets>()));
        }
         
        private static IInputService InputService()
        {
            if (Application.isEditor)
                return new StandaloneInputService();
            else
                return new MobileInputService();
        }
    }
}