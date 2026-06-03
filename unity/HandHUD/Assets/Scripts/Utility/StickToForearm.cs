using System.Collections;
using UnityEngine;

namespace Utility
{
    public class StickToForearm : MonoBehaviour
    {
        public OVRSkeleton ovrSkeleton;
    
        // best bone so far
        private const int WristParent = (int)OVRSkeleton.BoneId.Body_LeftHandWristTwist;

        public Vector3 positionOffset = new Vector3(0.05f, 0.02f, -0.17f);
        public Vector3 rotationOffset = new Vector3(63.3f, -180f, 0);

        IEnumerator Start()
        {
            if (ovrSkeleton == null)
                ovrSkeleton = FindAnyObjectByType<OVRSkeleton>();
            
            // skeleton might initialize late
            yield return new WaitUntil(() =>
                ovrSkeleton.IsInitialized && ovrSkeleton.IsDataValid);

            Transform wrist = ovrSkeleton.Bones[WristParent].Transform;

            transform.SetParent(wrist, false);
        }

        private void LateUpdate()
        {
            if (!transform.hasChanged) return;
            transform.localPosition = positionOffset;
            transform.localRotation = Quaternion.Euler(rotationOffset);
        }
    }
}