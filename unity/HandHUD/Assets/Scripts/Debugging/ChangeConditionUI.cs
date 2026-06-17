using System.Collections;
using Controller;
using Study;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Debugging
{
    public class ChangeConditionUI : MonoBehaviour
    {
        public OVRSkeleton ovrSkeleton;
        public Button waButton;
        public Button faButton;
        public Button hpButton;

        public TMP_Text header;

        private const int WristParent = (int)OVRSkeleton.BoneId.Body_RightHandWrist;

        public Vector3 positionOffset = new Vector3(0.23f, 0.13f, -0.08f);
        public Vector3 rotationOffset = new Vector3(0f, 126f, 0);

        private void OnEnable()
        {
            waButton.onClick.AddListener(SwitchToWA);
            faButton.onClick.AddListener(SwitchToFA);
            hpButton.onClick.AddListener(SwitchToHP);
        }

        private void OnDisable()
        {

            waButton.onClick.RemoveListener(SwitchToWA);
            faButton.onClick.RemoveListener(SwitchToFA);
            hpButton.onClick.RemoveListener(SwitchToHP);
        }

        private IEnumerator Start()
        {
            if (ovrSkeleton == null)
                ovrSkeleton = FindAnyObjectByType<OVRSkeleton>();

            // skeleton might initialize late
            yield return new WaitUntil(() =>
                ovrSkeleton.IsInitialized && ovrSkeleton.IsDataValid);

            Transform wrist = ovrSkeleton.Bones[WristParent].Transform;

            transform.SetParent(wrist, false);
            transform.localPosition = positionOffset;
            transform.localRotation = Quaternion.Euler(rotationOffset);
        }

        private void SwitchToHP()
        {
            header.text = "Current: HP";
            TaskController.Instance.SwitchCondition(Condition.HandProximal);
            TaskController.Instance.UpdateCurrentPanel(StudyConfig.GetPanelData(0));
        }

        private void SwitchToFA()
        {
            header.text = "Current: FA";
            TaskController.Instance.SwitchCondition(Condition.ForearmAnchored);
            TaskController.Instance.UpdateCurrentPanel(StudyConfig.GetPanelData(0));
        }

        private void SwitchToWA()
        {
            header.text = "Current: WA";
            TaskController.Instance.SwitchCondition(Condition.WorldAnchored);
            TaskController.Instance.UpdateCurrentPanel(StudyConfig.GetPanelData(0));
        }
    }
}