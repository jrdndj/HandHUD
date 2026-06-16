using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Controller
{
    public class NextTaskUI : MonoBehaviour
    {
        public OVRSkeleton ovrSkeleton;
        public Button nextTaskButton;

        private const int PalmParent = (int)OVRSkeleton.BoneId.Body_RightHandPalm;

        public Vector3 positionOffset = new(0.04f, -0.04f, 0f);
        public Vector3 rotationOffset = new(-87.7f, 79f, 192.4f);

        private void OnEnable()
        {
            TaskController.OnStateChanged += OnTaskControllerOnOnStateChanged;

            nextTaskButton.onClick.AddListener(
                TaskController.Instance.CompleteCurrentTask
            );
        }

        private void OnDisable()
        {
            TaskController.OnStateChanged -= OnTaskControllerOnOnStateChanged;

            nextTaskButton.onClick.RemoveListener(
                TaskController.Instance.CompleteCurrentTask
            );
        }

        private IEnumerator Start()
        {
            if (ovrSkeleton == null)
                ovrSkeleton = FindAnyObjectByType<OVRSkeleton>();

            // skeleton might initialize late
            yield return new WaitUntil(() =>
                ovrSkeleton.IsInitialized && ovrSkeleton.IsDataValid);

            Transform wrist = ovrSkeleton.Bones[PalmParent].Transform;

            transform.SetParent(wrist, false);
            transform.localPosition = positionOffset;
            transform.localRotation = Quaternion.Euler(rotationOffset);
        }

        private void OnTaskControllerOnOnStateChanged(StudyState state)
        {
            nextTaskButton.interactable = state switch
            {
                StudyState.RunningTask => true,
                StudyState.ConditionQuestionnaire or StudyState.Finished => false,
                _ => nextTaskButton.interactable
            };
        }
    }
}