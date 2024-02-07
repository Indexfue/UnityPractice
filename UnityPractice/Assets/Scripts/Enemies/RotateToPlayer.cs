using UnityEngine;
using UnityPractice.Infrastructure.Factories;
using UnityPractice.Infrastructure.Services;

namespace UnityPractice.Enemies
{
    public class RotateToPlayer : Follow
    {
        [SerializeField] private float speed;

        private Transform _heroTransform;
        private IGameFactory _gameFactory;
        private Vector3 _positionToLook;
        
        private float SpeedFactor => speed * Time.deltaTime;
        private bool IsInitialized => _heroTransform != null;

        private void Start()
        {
            _gameFactory = AllServices.Container.Single<IGameFactory>();

            if (IsHeroExist())
                InitializeHeroTransform();
            else
                _gameFactory.HeroCreated += OnHeroCreated;
        }

        private void Update()
        {
            if (IsInitialized)
                RotateTowardsHero();
        }

        private void OnDestroy()
        {
            if(_gameFactory != null)
                _gameFactory.HeroCreated -= OnHeroCreated;
        }

        private bool IsHeroExist() => 
            _gameFactory.HeroGameObject != null;

        private void RotateTowardsHero()
        {
            UpdatePositionToLookAt();

            transform.rotation = SmoothedRotation(transform.rotation, _positionToLook);
        }

        private void UpdatePositionToLookAt()
        {
            Vector3 positionDelta = _heroTransform.position - transform.position;
            _positionToLook = new Vector3(positionDelta.x, transform.position.y, positionDelta.z);
        }
    
        private Quaternion SmoothedRotation(Quaternion rotation, Vector3 positionToLook) =>
            Quaternion.Lerp(rotation, TargetRotation(positionToLook), SpeedFactor);

        private Quaternion TargetRotation(Vector3 position) =>
            Quaternion.LookRotation(position);

    
        private void OnHeroCreated() =>
            InitializeHeroTransform();

        private void InitializeHeroTransform() =>
            _heroTransform = _gameFactory.HeroGameObject.transform;
    }
}