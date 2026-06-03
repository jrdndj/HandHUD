using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public enum PanelType
    {
        ImageOnly,
        TextOnly,
        ImageAndText,
    }

    [System.Serializable]
    public class MarkerData
    {
        public int id;
        public Vector3 positionOffset = new Vector3(0.6f, 0.23f, 0f); // floating above a bit
        public Vector3 rotationOffset = Vector3.zero;
        public string message = "";
        public Sprite sprite;
        public Color color = Color.orange;
        public PanelType panelType = PanelType.ImageAndText;

        public static readonly MarkerData Invalid = new MarkerData
        {
            id = -1,
            message = "INVALID MARKER",
            positionOffset = Vector3.zero,
            rotationOffset = Vector3.zero,
            color = Color.red,
            panelType = PanelType.TextOnly
        };
    }

    [CreateAssetMenu(menuName = "Markers/Marker Database")]
    public class MarkerDatabase : ScriptableObject
    {
        public List<MarkerData> markers;
    }
}