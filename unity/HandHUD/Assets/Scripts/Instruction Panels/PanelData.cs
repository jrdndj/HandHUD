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
        public string message;
        public Sprite sprite;
        public Color color;
        public PanelType panelType;

        private const int AlphaValue = 230;

        public PanelData(Color color, string message, PanelType panelType, Sprite sprite, bool slightlyTransparent = true)
        {
            this.color = color;
            this.message = message;
            this.panelType = panelType;
            this.sprite = sprite;

            if (slightlyTransparent) this.color.a = AlphaValue;
        }

        public static readonly PanelData Invalid = new(
            Color.red,
            "INVALID MARKER",
            PanelType.TextOnly,
            null
        );
    }
}