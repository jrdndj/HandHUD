using System.Collections;
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

        // best bone so far
        private const int WristParent = (int)OVRSkeleton.BoneId.Body_RightHandWrist;

        public Vector3 positionOffset = new Vector3(0.23f, 0.13f, -0.08f);
        public Vector3 rotationOffset = new Vector3(0f, 126f, 0);

         IEnumerator Start()
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

            waButton.onClick.AddListener(() =>
            {
                header.text = "Current: WA";
                SceneController.Instance.SwitchCondition(Condition.WorldAnchored);
            });
            faButton.onClick.AddListener(() =>
            {
                header.text = "Current: FA";
                SceneController.Instance.SwitchCondition(Condition.ForearmAnchored);
            });
            hpButton.onClick.AddListener(() =>
            {
                header.text = "Current: HP";
                SceneController.Instance.SwitchCondition(Condition.HandProximal);
            });
        }
    }
}