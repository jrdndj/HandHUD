using UnityEngine;

namespace Utility
{
    public class FloatNearHand : MonoBehaviour
    {
        public GameObject handAnchor;
        public Vector3 positionOffset = new(-0.02f, 0.2f, 0.2f);
        public Camera camera;

        private const string HandAnchorName = "LeftHandAnchor";

        private void OnEnable()
        {
            camera = Camera.main;

            if (!camera)
            {
                enabled = false;
                Debug.Log("Failed to find Main Camera in scene.");
                return;
            }

            handAnchor = GameObject.Find(HandAnchorName);

            if (!handAnchor)
            {
                enabled = false;
                Debug.Log("Failed to find LeftHandAnchor object in scene.");
            }
        }

        private void LateUpdate()
        {
            var head = camera.transform;

            var horizontalForward = Vector3.ProjectOnPlane(head.forward, Vector3.up).normalized;
            var horizontalRight = Vector3.Cross(Vector3.up, horizontalForward);

            transform.position =
                handAnchor.transform.position +
                horizontalRight * positionOffset.x +
                Vector3.up * positionOffset.y +
                horizontalForward * positionOffset.z;
        }
    }
}