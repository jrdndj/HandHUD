using System;
using System.Collections;
using Study;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Controller
{
    public class NextTaskUI : MonoBehaviour
    {
        public OVRSkeleton ovrSkeleton;

        private const int PalmParent = (int)OVRSkeleton.BoneId.Body_RightHandPalm;
        private Transform _wrist;

        public Vector3 positionOffset = new(0.04f, -0.04f, 0f);
        public Vector3 rotationOffset = new(-87.7f, 79f, 192.4f);

        public float showThreshold = -0.06f;

        public CanvasGroup canvasGroup;
        public Button nextTaskButton;
        public TMP_Text infoLabel;

        private int _totalTasks;
        private int _tasksDone = 0;

        private void Awake()
        {
            nextTaskButton.interactable = false;
        }

        private void OnEnable()
        {
            _tasksDone = 0;
            _totalTasks = StudyConfig.TasksPerCondition * StudyConfig.ConditionsPerSuperBlock *
                          StudyConfig.SuperBlockCount;

            TaskController.Instance.OnStateChanged += OnTaskControllerStateChanged;
            TaskController.Instance.OnTaskComplete += OnTaskComplete;

            nextTaskButton.onClick.AddListener(
                TaskController.Instance.CompleteCurrentTask
            );
        }

        private void OnDisable()
        {
            TaskController.Instance.OnStateChanged -= OnTaskControllerStateChanged;
            TaskController.Instance.OnTaskComplete += OnTaskComplete;

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

            _wrist = ovrSkeleton.Bones[PalmParent].Transform;

            transform.SetParent(_wrist, false);
            transform.localPosition = positionOffset;
            transform.localRotation = Quaternion.Euler(rotationOffset);

            infoLabel.text = $"{_tasksDone} / {_totalTasks}";
        }

        private void Update()
        {
            if (!_wrist) return;

            float facingUp = Vector3.Dot(
                _wrist.up,
                Vector3.up);

            bool visible = facingUp < showThreshold;

            canvasGroup.alpha = visible ? 1f : 0f;
            canvasGroup.interactable = visible;
            canvasGroup.blocksRaycasts = visible;
        }

        private void OnTaskControllerStateChanged(StudyState state)
        {
            nextTaskButton.interactable = state switch
            {
                StudyState.RunningTask => true,
                StudyState.ConditionQuestionnaire or StudyState.Finished => false,
                _ => nextTaskButton.interactable
            };
        }

        private void OnTaskComplete(int sb, int cond, int task)
        {
            _tasksDone++;
            infoLabel.text = $"{_tasksDone} / {_totalTasks}";
        }
    }
}