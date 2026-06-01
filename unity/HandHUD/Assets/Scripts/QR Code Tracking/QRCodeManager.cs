using Meta.XR.MRUtilityKit;
using UnityEngine;
using System;
using QR_Code_Tracking;
using UnityEngine.Serialization;

public class QRCodeManager : MonoBehaviour
{
    [SerializeField] private GameObject qrCodePrefab;
    [SerializeField] private MRUK mrukInstance;
    
    private void OnEnable()
    {
        if (!mrukInstance)
        {
            Debug.Log($"QRCodeManager requires an MRUK object in the scene!");
            return;
        }

        mrukInstance.SceneSettings.TrackableAdded.AddListener(OnTrackableAdded);
        mrukInstance.SceneSettings.TrackableRemoved.AddListener(OnTrackableRemoved);
    }
    
    public void OnTrackableAdded(MRUKTrackable trackable)
    {
        if (trackable.TrackableType != OVRAnchor.TrackableType.QRCode ||
            trackable.MarkerPayloadString == null) return;
        
        var instance = Instantiate(qrCodePrefab, trackable.transform);
        // TODO: make QRCode class
        var qrCode = instance.GetComponent<QRCode>();
        // TODO: store payload data
        qrCode.Initialize(trackable);
        // TODO: need?
        // instance.GetComponent<Bounded2DVisualizer>().Initialize(trackable);
        
        Debug.Log($"Detected QR code: {trackable.MarkerPayloadString}");
    }
    
    public void OnTrackableRemoved(MRUKTrackable trackable)
    {
        if (trackable.TrackableType != OVRAnchor.TrackableType.QRCode ||
            trackable.MarkerPayloadString == null) return;
        
        Debug.Log($"Trackable removed: {trackable.name}");
        Destroy(trackable.gameObject);
    }
}
