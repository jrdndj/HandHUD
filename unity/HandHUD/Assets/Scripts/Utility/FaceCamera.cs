using UnityEngine;

namespace Utility
{
    public class FaceCamera : MonoBehaviour
    {
        public Transform cameraTransform;

        private void Start()
        {
            cameraTransform = Camera.main?.transform;

            if (cameraTransform != null) return;

            Debug.LogWarning("No Main Camera found.");
            enabled = false;

            cameraTransform = Camera.main.transform;
        }

        private void LateUpdate()
        {
            Vector3 dir = transform.position - cameraTransform.position;

            transform.rotation = Quaternion.LookRotation(dir);
        }
    }
}