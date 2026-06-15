using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Controller
{
    public class NextTaskUI : MonoBehaviour
    {
        public OVRSkeleton ovrSkeleton;
        public Button nextTaskButton;

        private const int PalmParent = (int)OVRSkeleton.BoneId.Body_RightHandPalm;

        public Vector3 positionOffset = new Vector3(0.3f, 0.72f, -0.26f);
        public Vector3 rotationOffset = new Vector3(-31f, 141f, 34f);

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

            nextTaskButton.onClick.AddListener(() =>
            {
                TaskController.Instance.ProceedToNextTask();
            });
        }
    }
}