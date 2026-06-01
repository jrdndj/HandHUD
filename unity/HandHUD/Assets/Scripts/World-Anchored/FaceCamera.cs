using UnityEngine;
using UnityEngine.Serialization;

public class FaceCamera : MonoBehaviour
{
    public Transform target;
    public Vector3 positionOffset = Vector3.up * 0.2f;
    public Vector3 rotationOffset = Vector3.zero;

    private void LateUpdate()
    {
        transform.position = target.position + positionOffset;

        Vector3 dir =
            transform.position - Camera.main.transform.position;

        transform.rotation =
            Quaternion.LookRotation(dir) *
            Quaternion.Euler(rotationOffset);
    }
}
