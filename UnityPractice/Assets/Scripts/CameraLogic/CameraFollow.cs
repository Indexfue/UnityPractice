using UnityEngine;

namespace UnityPractice.CameraLogic
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private float rotationAngleX;
        [SerializeField] private int distance;
        [SerializeField] private float offsetY;

        private Transform _followingObject;

        private void LateUpdate()
        {
            if(_followingObject == null)
                return;
            Quaternion rotation = Quaternion.Euler(rotationAngleX, 0, 0);
            Vector3 position = rotation * new Vector3(0, 0, -distance) + FollowingPointPosition();
            transform.rotation = rotation;
            transform.position = position;
        }

        public void Follow(GameObject following)
        {
            this._followingObject = following.transform;
        }
        private Vector3 FollowingPointPosition()
        {
            Vector3 followingPosition = _followingObject.position;
            followingPosition.y += offsetY;
            return followingPosition;
        }
    }
}