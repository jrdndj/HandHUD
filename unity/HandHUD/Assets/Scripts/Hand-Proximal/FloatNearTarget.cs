using UnityEngine;
using UnityEngine.Serialization;

public class FloatNearTarget : MonoBehaviour
{
    public Transform target;
    public Vector3 positionOffset = Vector3.up * 0.2f;

    private void LateUpdate()
    {
        transform.position = target.position + positionOffset;
    }
}