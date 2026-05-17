using TMPro;
using UnityEngine;
using Meta.XR.BuildingBlocks;
using UnityEngine.Assertions;
using UnityEngine.Serialization;
using static OVRSpatialAnchor;

// Controls the text shown on the spatial anchors
public class AnchorLabelManager : MonoBehaviour
{
    [SerializeField] private SpatialAnchorCoreBuildingBlock spatialAnchorCore;
    private AnchorText _previewAnchorText;

    private void Start()
    {
        // find preview anchor (should be under right hand building block)
        _previewAnchorText = FindAnyObjectByType<AnchorText>();
        Assert.IsNotNull(_previewAnchorText);

        RefreshAnchor(_previewAnchorText);
    }

    private void OnEnable()
    {
        spatialAnchorCore.OnAnchorCreateCompleted.AddListener(HandleAnchorCreated);
        spatialAnchorCore.OnAnchorsEraseAllCompleted.AddListener(HandleAnchorsDeleted);
    }

    private void OnDisable()
    {
        spatialAnchorCore.OnAnchorCreateCompleted.RemoveListener(HandleAnchorCreated);
        spatialAnchorCore.OnAnchorsEraseAllCompleted.RemoveListener(HandleAnchorsDeleted);
    }

    private void HandleAnchorCreated(OVRSpatialAnchor anchor, OperationResult result)
    {
        if (result != OperationResult.Success)
        {
            Debug.Log("Failed to create anchor.");
            return;
        }

        var anchorText = anchor.gameObject.GetComponent<AnchorText>();
        
        // update new anchor with preview's string
        RefreshAnchor(anchorText);

        // load preview anchor with next string
        GlobalTextState.Advance();
        RefreshAnchor(_previewAnchorText);
    }

    private void HandleAnchorsDeleted(OperationResult result)
    {
        if (result != OperationResult.Success)
            return;
        
        GlobalTextState.Reset();
    }

    private void RefreshAnchor(AnchorText anchorText)
    {
        anchorText.SetText(GlobalTextState.CurrentString);
    }
}