using UnityEngine;
using UnityEngine.AI;
using UnityPractice.Infrastructure.Factories;
using UnityPractice.Infrastructure.Services;

namespace UnityPractice.Enemies
{
    public class AgentMoveToPlayer : MonoBehaviour
    {
        private const float MinimalDistance = 1;
        
        [SerializeField] private NavMeshAgent agent;

        private Transform _heroTransform;
        private IGameFactory _gameFactory;
        
        private bool IsHeroNotReached =>
            Vector3.Distance(agent.transform.position, _heroTransform.position) >= MinimalDistance;
        
        private bool IsHeroTransformInitialized => _heroTransform != null;

        private void Start()
        {
            _gameFactory = AllServices.Container.Single<IGameFactory>();

            if (_gameFactory.HeroGameObject != null) 
                InitializeHeroTransform();
            else
                _gameFactory.HeroCreated += OnHeroCreated;
        }

        private void Update()
        {
            if (IsHeroTransformInitialized && IsHeroNotReached)
                agent.destination = _heroTransform.position;
        }

        private void OnHeroCreated() => 
            InitializeHeroTransform();

        private void InitializeHeroTransform() => 
            _heroTransform = _gameFactory.HeroGameObject.transform;
    }
}