using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Core
{
    public class InstructionPanel : MonoBehaviour
    {
        [SerializeField] private TMP_Text canvasText;
        [SerializeField] private Image canvasImg;
        [SerializeField] private Image canvasBg;

        public void UpdateData(MarkerData markerData)
        {
            SetText(markerData.message);
            SetSprite(markerData.sprite);
            SetColor(markerData.color);
        }

        public void SetText(string text)
        {
            if (canvasText != null)
                canvasText.text = text;
        }

        public void SetSprite(Sprite sprite)
        {
            if (canvasImg != null && sprite != null)
                canvasImg.sprite = sprite;
        }

        public void SetColor(Color color)
        {
            canvasBg.color = color;
        }
    }
}