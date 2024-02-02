using UnityPractice.Infrastructure.Factories;
using UnityEngine;
using UnityPractice.CameraLogic;
using UnityPractice.Character;
using UnityPractice.Logic;

namespace UnityPractice.Infrastructure.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private const string InitialPointTag = "PlayerInitialPoint";

        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _loadingCurtain;
        private readonly IGameFactory _gameFactory;

        public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, LoadingCurtain loadingCurtain, IGameFactory gameFactory)
        {
            _stateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _loadingCurtain = loadingCurtain;
            _gameFactory = gameFactory;
        }

        public void Enter(string sceneName)
        {
            _loadingCurtain.Show();
            _sceneLoader.Load(sceneName, OnLoaded);
        }

        public void Exit()
        {
            _loadingCurtain.Hide();
        }

        private void OnLoaded()
        {
            GameObject characterInstance = _gameFactory.CreateHero(GameObject.FindGameObjectWithTag(InitialPointTag));
            _gameFactory.CreateHud();

            Camera camera = Camera.main;
            characterInstance.GetComponent<CharacterMover>().Construct(camera);
            CameraFollow(camera, characterInstance);

            _stateMachine.EnterIn<GameLoopState>();
        }

        private void CameraFollow(Camera camera, GameObject objectInstance)
        {
            if (camera.TryGetComponent(out CameraFollow cameraFollow))
            {
                cameraFollow.Follow(objectInstance);
            }
        }
    }
}