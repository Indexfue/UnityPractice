using Data;
using UnityPractice.CameraLogic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityPractice.Infrastructure;
using UnityPractice.Infrastructure.Services;

namespace UnityPractice.Character
{
    public class CharacterMover : MonoBehaviour, ISavedProgress
    {
        [SerializeField] private CharacterController characterController;
        [SerializeField] private float movementSpeed;

        private Camera _camera;
        private IInputService _inputService;
        private Vector3 _rawMovementVector;

        private Vector3 MovementVector => _rawMovementVector * Time.deltaTime;

        public void Construct(Camera camera)
        {
            _camera = camera;
        }

        public void Load(PlayerProgress playerProgress)
        {
            if (CurrentLevel() == playerProgress.WorldData.PositionOnLevel.Level)
            {
                Vector3Data savedPosition = playerProgress.WorldData.PositionOnLevel.Position;
                if (savedPosition != null) 
                    Warp(savedPosition);
            }
        }

        public void SaveProgress(PlayerProgress playerProgress) => 
            playerProgress.WorldData.PositionOnLevel = new PositionOnLevel(CurrentLevel(), transform.position.AsWorldVector3());

        private void Warp(Vector3Data to)
        {
            characterController.enabled = false;
            transform.position = to.AsUnityVector3().AddY(characterController.height);
            characterController.enabled = true;
        }

        private static string CurrentLevel() => 
            SceneManager.GetActiveScene().name;

        private void Awake()
        {
            _inputService = AllServices.Container.Single<IInputService>();
        }

        private void Update()
        {
            _rawMovementVector = Vector3.zero;

            if (_inputService.Axis.sqrMagnitude > Mathf.Epsilon)
            {
                _rawMovementVector = _camera.transform.TransformDirection(_inputService.Axis);
                _rawMovementVector.y = 0f;
                _rawMovementVector.Normalize();

                transform.forward = _rawMovementVector;
            }

            _rawMovementVector += Physics.gravity;
            characterController.Move(MovementVector * movementSpeed);
        }
    }
}