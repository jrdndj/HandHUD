using Instruction_Panels;
using Meta.XR.MRUtilityKit;
using Study;
using UnityEngine;
using Utility;

namespace Conditions
{
    public class WorldAnchoredManager : ConditionManager
    {
        public Vector3 positionOffset = Vector3.zero; // floating above a bit
        public Vector3 rotationOffset = Vector3.zero;

        [SerializeField] private MRUK mrukInstance;

        private void Awake()
        {
            SetMrukTracking(false);
            mrukInstance.SceneSettings.TrackableAdded.AddListener(OnTrackableAdded);
            mrukInstance.SceneSettings.TrackableRemoved.AddListener(OnTrackableRemoved);
        }

        protected override void OnActivated()
        {
            if (!mrukInstance)
            {
                return;
            }

            SetMrukTracking(true);
        }

        protected override void OnDeactivated()
        {
            if (!mrukInstance)
                return;

            SetMrukTracking(false);
        }

        private void SetMrukTracking(bool value)
        {
            var tc = mrukInstance.SceneSettings.TrackerConfiguration;
            tc.QRCodeTrackingEnabled = value;
            mrukInstance.SceneSettings.TrackerConfiguration = tc;
        }

        private void OnTrackableAdded(MRUKTrackable trackable)
        {
            if (trackable.TrackableType != OVRAnchor.TrackableType.QRCode ||
                trackable.MarkerPayloadString == null) return;

            int markerId = ParsePayload(trackable);

            PanelData markerData = StudyConfig.GetPanelData(markerId);
            InstructionPanel prefab = GetPanelPrefab(markerData.panelType);

            InstructionPanel instance = Instantiate(prefab, trackable.transform);

            instance.GetComponent<FaceCamera>().enabled = true;
            instance.transform.localPosition = positionOffset;
            instance.transform.localRotation = Quaternion.Euler(rotationOffset);

            instance.UpdateData(markerData);
        }

        private void OnTrackableRemoved(MRUKTrackable trackable)
        {
            if (trackable.TrackableType != OVRAnchor.TrackableType.QRCode ||
                trackable.MarkerPayloadString == null) return;

            Destroy(trackable.gameObject);
        }

        private int ParsePayload(MRUKTrackable trackable)
        {
            return int.TryParse(trackable.MarkerPayloadString, out var value)
                ? value
                : -1;
        }
    }
}