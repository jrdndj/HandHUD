using UnityEngine;
using UnityEngine.Serialization;

namespace Utility
{
    public class FaceCamera : MonoBehaviour
    {
        public new Camera camera;

        private void Start()
        {
            camera = Camera.main;

            if (camera == null)
            {
                enabled = false;
                Debug.Log("Failed to find Main Camera in scene.");
            }
        }

        private void LateUpdate()
        {
            Vector3 dir = transform.position - camera.transform.position;

            transform.rotation = Quaternion.LookRotation(dir);
        }
    }
}