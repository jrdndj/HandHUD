using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Instruction_Panels
{
    public class InstructionPanel : MonoBehaviour
    {
        [SerializeField] private TMP_Text canvasText;
        [SerializeField] private Image canvasImg;
        [SerializeField] private Image canvasBg;

        public void UpdateData(PanelData markerData)
        {
            SetText(markerData.message);
            SetSprite(markerData.sprite);
            SetColor(markerData.color);
        }

        private void SetText(string text)
        {
            if (canvasText != null)
                canvasText.text = text;
        }

        private void SetSprite(Sprite sprite)
        {
            if (canvasImg != null && sprite != null)
                canvasImg.sprite = sprite;
        }

        private void SetColor(Color color)
        {
            canvasBg.color = color;
        }
    }
}