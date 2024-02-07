using System;
using UnityEngine;

namespace UnityPractice.Enemies
{
    public class Aggro : MonoBehaviour
    {
        [SerializeField] private TriggerObserver triggerObserver;
        [SerializeField] private AgentMoveToPlayer follow;
        
        private void Start()
        {
            triggerObserver.TriggerEntered += OnTriggerEntered;
            triggerObserver.TriggerExited += OnTriggerExited;

            follow.enabled = false;
        }

        private void OnTriggerEntered(Collider other) => 
            follow.enabled = true;

        private void OnTriggerExited(Collider other) => 
            follow.enabled = false;
    }
}