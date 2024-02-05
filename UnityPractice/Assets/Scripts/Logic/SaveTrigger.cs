using System;
using UnityEngine;
using UnityPractice.Character;
using UnityPractice.Infrastructure.Services;
using UnityPractice.Infrastructure.Services.SaveLoad;

namespace UnityPractice.Logic
{
    public class SaveTrigger : MonoBehaviour
    {
        [SerializeField] private BoxCollider boxCollider;
        
        private ISaveLoadService _saveLoadService;
        
        private void Awake()
        {
            _saveLoadService = AllServices.Container.Single<ISaveLoadService>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out ISavedProgress progressWriter))
            {
                _saveLoadService.SaveProgress();
                Debug.Log("Progress saved");
                gameObject.SetActive(false);
            }
        }

        private void OnDrawGizmos()
        {
            if (boxCollider == null)
                return;
            
            Gizmos.color = new Color32(30, 200, 30, 130);
            Gizmos.DrawCube(transform.position, boxCollider.size);
            Gizmos.color = Color.white;
        }
    }
}