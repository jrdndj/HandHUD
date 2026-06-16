using System;
using Instruction_Panels;
using Study;
using UnityEngine;

namespace Controller
{
    public class TaskTracker : MonoBehaviour
    {
        private void OnEnable()
        {
            TaskController.OnTaskComplete += OnTaskControllerOnOnTaskComplete;
        }

        private void OnDisable()
        {
            TaskController.OnTaskComplete -= OnTaskControllerOnOnTaskComplete;
        }

        private void OnTaskControllerOnOnTaskComplete(int superBlockIdx, int conditionIdx, int taskIdx)
        {
            Debug.Log($"TaskTracker: Superblock {superBlockIdx}, Condition {conditionIdx}, Task {taskIdx}");
        }
    }
}