using Meta.XR.MRUtilityKit;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace QR_Code_Tracking
{
    public class QRCode : MonoBehaviour
    {
        public string PayloadText => canvasText.text;
        
        // TODO: make these in prefab
        [SerializeField] private TMP_Text canvasText;
        [SerializeField] private Image canvasImg;
        
        public void Initialize(MRUKTrackable trackable)
        {
        }
    }
}