using UnityEngine;
using UnityPractice.CameraLogic;
using UnityPractice.Character;
using UnityPractice.Infrastructure.Services.PersistentProgress;
using UnityPractice.Logic;

namespace UnityPractice.Infrastructure.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private const string InitialPointTag = "PlayerInitialPoint";
        private const string CharacterPath = "Hero/hero";
        private const string HudPath = "Hud/hud";

        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _loadingCurtain;
        private readonly IGameFactory _gameFactory;
        private readonly IPersistentProgressService _progressService;

        public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, LoadingCurtain loadingCurtain, IGameFactory gameFactory, IPersistentProgressService progressService)
        {
            _stateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _loadingCurtain = loadingCurtain;
            _gameFactory = gameFactory;
            _progressService = progressService;
        }

        public void Enter(string sceneName)
        {
            _loadingCurtain.Show();
            _gameFactory.CleanUp();
            _sceneLoader.Load(sceneName, OnLoaded);
        }

        public void Exit()
        {
            _loadingCurtain.Hide();
        }

        private void OnLoaded()
        {
            InitGameWorld();
            InformProgressReaders();

            _stateMachine.EnterIn<GameLoopState>();
        }

        private void InformProgressReaders()
        {
            foreach (ISavedProgressReader progressReader in _gameFactory.ProgressReaders)
            {
                progressReader.Load(_progressService.PlayerProgress);
            }
        }

        private void InitGameWorld()
        {
            GameObject characterInstance = _gameFactory.CreateHero(GameObject.FindGameObjectWithTag(InitialPointTag));
            _gameFactory.CreateHud();

            Camera camera = Camera.main;
            characterInstance.GetComponent<CharacterMover>().Construct(camera);
            CameraFollow(camera, characterInstance);
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