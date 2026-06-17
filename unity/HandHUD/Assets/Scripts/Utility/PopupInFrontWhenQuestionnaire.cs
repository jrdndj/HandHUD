using Controller;
using UnityEngine;

namespace Utility
{
    public class PopupInFrontWhenQuestionnaire : MonoBehaviour
    {
        [SerializeField] private new Camera camera;

        private void OnEnable()
        {
            TaskController.Instance.OnStateChanged += OnTaskControllerOnOnStateChanged;
        }

        private void OnDisable()
        {
            TaskController.Instance.OnStateChanged -= OnTaskControllerOnOnStateChanged;
        }

        private void Start()
        {
            camera = Camera.main;

            if (camera == null)
            {
                enabled = false;
                Debug.Log("Failed to find Main Camera in scene.");
            }
        }

        private void OnTaskControllerOnOnStateChanged(StudyState state)
        {
            if (state == StudyState.ConditionQuestionnaire)
            {
                PopupQuestionnaire();
            }
        }

        private void PopupQuestionnaire()
        {
            const float distance = 0.6f;
            const float verticalOffset = -0.2f;

            Vector3 forward = camera.transform.forward;
            forward.y = 0f;
            forward.Normalize();

            transform.position =
                camera.transform.position +
                forward * distance +
                Vector3.up * verticalOffset;

            Vector3 toUser = camera.transform.position - transform.position;
            toUser.y = 0f;

            transform.rotation =
                Quaternion.LookRotation(-toUser, Vector3.up);
        }
    }
}