using UnityEngine;
using UnityPractice.CameraLogic;
using UnityPractice.Character;
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

        public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, LoadingCurtain loadingCurtain)
        {
            _stateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _loadingCurtain = loadingCurtain;
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
            GameObject playerInitialPoint = GameObject.FindGameObjectWithTag(InitialPointTag);
            GameObject characterInstance = Instantiate(CharacterPath, playerInitialPoint.transform.position);
            Instantiate(HudPath);

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

        private GameObject Instantiate(string path)
        {
            var prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab);
        }

        private GameObject Instantiate(string path, Vector3 at)
        {
            var prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab, at, Quaternion.identity);
        }
    }
}