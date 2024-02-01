using UnityPractice.CameraLogic;
using UnityEngine;
using UnityPractice.Infrastructure;

namespace UnityPractice.Character
{
    public class CharacterMover : MonoBehaviour
    {
        [SerializeField] private CharacterController characterController;
        [SerializeField] private float movementSpeed;

        private Camera _camera;
        private IInputService _inputService;
        private Vector3 _rawMovementVector;

        private Vector3 MovementVector => _rawMovementVector * Time.deltaTime;

        private void Awake()
        {
            _inputService = Game.InputService;
        }

        private void Start()
        {
            _camera = Camera.main;
            CameraFollow();
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

        private void CameraFollow()
        {
            if (_camera.TryGetComponent(out CameraFollow cameraFollow))
            {
                cameraFollow.Follow(gameObject);
            }
        }
    }
}