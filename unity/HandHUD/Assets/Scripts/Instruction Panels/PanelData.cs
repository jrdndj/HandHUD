using UnityEngine;

namespace Instruction_Panels
{
    public enum PanelType
    {
        ImageOnly,
        TextOnly,
        ImageAndText,
    }

    [System.Serializable]
    public class PanelData
    {
        public string message = "";
        public Sprite sprite;
        public Color color = Color.orange;
        public PanelType panelType = PanelType.ImageAndText;

        public static readonly PanelData Invalid = new()
        {
            message = "INVALID MARKER",
            color = Color.red,
            panelType = PanelType.TextOnly
        };
    }
}