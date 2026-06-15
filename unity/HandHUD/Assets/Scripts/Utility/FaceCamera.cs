using UnityEngine;

namespace Utility
{
    public class FaceCamera : MonoBehaviour
    {
        public Transform cameraTransform;

        private void Start()
        {
            cameraTransform = Camera.main?.transform;

            if (cameraTransform != null)
            {
                enabled = false;
                Debug.Log("Failed to find Main Camera in scene.");
                return;
            }

            Debug.LogWarning("No Main Camera found.");
            enabled = false;
        }

        private void LateUpdate()
        {
            Vector3 dir = transform.position - cameraTransform.position;

            transform.rotation = Quaternion.LookRotation(dir);
        }
    }
}