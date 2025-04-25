using UnityEngine;

public class RotateSelf : MonoBehaviour
{
    public Vector3 localRotationSpeed = new Vector3(0f, 90f, 0f); // degrees per second

    void Update()
    {
        // Rotate around local axes
        transform.Rotate(localRotationSpeed * Time.deltaTime, Space.Self);
    }
}
