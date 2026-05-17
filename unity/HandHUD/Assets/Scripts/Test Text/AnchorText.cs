using TMPro;
using UnityEngine;

public class AnchorText : MonoBehaviour
{
    [SerializeField] private TMP_Text label;
    
    private void Start() {}

    private void Update() {}

    public void SetText(string text)
    {
        label.text = text;
    }
}
