using System;
using System.Collections;
using UnityEngine;

namespace UnityPractice.Enemies
{
    public class Aggro : MonoBehaviour
    {
        [SerializeField] private TriggerObserver triggerObserver;
        [SerializeField] private Follow follow;
        [SerializeField, Min(0.1f)] private float aggroTime;

        private Coroutine _aggroCoroutine;
        private bool _hasAggroObject;

        private void Start()
        {
            triggerObserver.TriggerEntered += OnTriggerEntered;
            triggerObserver.TriggerExited += OnTriggerExited;

            follow.enabled = false;
        }

        private void OnTriggerEntered(Collider other)
        {
            if (!_hasAggroObject)
            {
                _hasAggroObject = true;
                follow.enabled = true;
                StopAggroCoroutine();
            }
        }

        private void OnTriggerExited(Collider other)
        {
            if (_hasAggroObject)
            {
                _hasAggroObject = false;
                _aggroCoroutine = StartCoroutine(DisableFollowIfAggroTimeExpired());
            }
        }

        private IEnumerator DisableFollowIfAggroTimeExpired()
        {
            yield return new WaitForSeconds(aggroTime);
            follow.enabled = false;
        }

        private void StopAggroCoroutine()
        {
            if (_aggroCoroutine != null)
            {
                StopCoroutine(_aggroCoroutine);
                _aggroCoroutine = null;
            }
        }
    }
}