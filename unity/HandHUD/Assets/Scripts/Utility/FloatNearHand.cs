using UnityEngine;

namespace Utility
{
    public class FloatNearHand : MonoBehaviour
    {
        public GameObject handAnchor;
        public Vector3 positionOffset = new (-0.02f, 0.2f, 0.2f);

        private const string HandAnchorName = "LeftHandAnchor";

        private void OnEnable()
        {
            handAnchor = GameObject.Find(HandAnchorName);
            
            if (!handAnchor)
            {
                enabled = false;
                Debug.Log("Failed to find LeftHandAnchor object in scene.");
            }
        }

        private void LateUpdate()
        {
            transform.position = handAnchor.transform.position + positionOffset;
        }
    }
}