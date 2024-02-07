using UnityEngine;
using UnityEngine.AI;

namespace UnityPractice.Enemies
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(EnemyAnimator))]
    public class AnimateAlongAgent : MonoBehaviour
    {
        private const float MinimalVelocity = 0.1f;
        
        [SerializeField] private NavMeshAgent agent;
        [SerializeField] private EnemyAnimator enemyAnimator;

        private bool IsShouldMove =>
            agent.velocity.magnitude > MinimalVelocity && agent.remainingDistance > agent.radius;

        private void Update()
        {
            if (IsShouldMove)
                enemyAnimator.Move(agent.velocity.magnitude);
            else
                enemyAnimator.StopMoving();
        }
    }
}