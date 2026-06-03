using System.Collections.Generic;
using Meta.XR.MRUtilityKit;
using UnityEngine;
using Utility;

namespace Managers
{
    public class WAConditionManager : ConditionManagerBase
    {
        [SerializeField] private MRUK mrukInstance;

        private bool _disabled = true;

        protected override void OnActivated()
        {
            _disabled = false;

            if (!mrukInstance)
                return;

            mrukInstance.gameObject.SetActive(true);

            mrukInstance.SceneSettings.TrackableAdded.AddListener(OnTrackableAdded);
            mrukInstance.SceneSettings.TrackableRemoved.AddListener(OnTrackableRemoved);
        }

        protected override void OnDeactivated()
        {
            if (_disabled) return;
            _disabled = true;

            if (!mrukInstance)
                return;

            mrukInstance.gameObject.SetActive(false);

            mrukInstance.SceneSettings.TrackableAdded.RemoveListener(OnTrackableAdded);
            mrukInstance.SceneSettings.TrackableRemoved.RemoveListener(OnTrackableRemoved);

            mrukInstance.ClearScene();
        }

        private void OnTrackableAdded(MRUKTrackable trackable)
        {
            if (trackable.TrackableType != OVRAnchor.TrackableType.QRCode ||
                trackable.MarkerPayloadString == null) return;

            int markerId = ParsePayload(trackable);

            var markerData = GetMarkerData(markerId);
            var prefab = GetPanelPrefab(markerData.panelType);

            var instance = Instantiate(prefab, trackable.transform);

            instance.GetComponent<FaceCamera>().enabled = true;
            instance.transform.localPosition = markerData.positionOffset;
            instance.transform.localRotation = Quaternion.Euler(markerData.rotationOffset);

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
            var payload = trackable.MarkerPayloadString;

            if (int.TryParse(payload, out var value))
                return value;
            else return -1;
        }
    }
}