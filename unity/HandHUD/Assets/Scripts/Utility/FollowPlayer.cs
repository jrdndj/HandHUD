using UnityEngine;

namespace Utility
{
    public class LerpToTarget : MonoBehaviour
    {
        public Transform target;

        // How often we check for repositioning
        public float repositionInterval = 5f;

        // How far the player must move before UI follows
        public float followThreshold = 0.5f;

        // How quickly the object moves to target position
        public float smoothSpeed = 2f;

        // Offset from player/camera
        public Vector3 offset = new Vector3(0f, 0f, 0.8f);

        private float _timer;

        private Vector3 _desiredPosition;

        private void Start()
        {
            _desiredPosition = transform.position;
        }

        private void Update()
        {
            _timer += Time.deltaTime;

            // Every few seconds, decide whether to update target position
            if (_timer >= repositionInterval)
            {
                Vector3 candidatePosition = target.position + offset;

                // Only move if far enough away
                float distance = Vector3.Distance(transform.position, candidatePosition);

                if (distance > followThreshold)
                {
                    _desiredPosition = candidatePosition;
                }

                _timer = 0f;
            }

            // Smooth movement every frame
            transform.position = Vector3.Lerp(
                transform.position,
                _desiredPosition,
                smoothSpeed * Time.deltaTime
            );
        }
    }
}
