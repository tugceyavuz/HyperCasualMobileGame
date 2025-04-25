using UnityEngine;

public class CameraFollowZAxis : MonoBehaviour
{
    public Transform player;  // The player's transform (assign in Inspector)
    public float followDistance = 10f;  // The fixed distance along the Z-axis (adjust in Inspector)

    void LateUpdate()
    {
        if (player != null)
        {
            // Follow the player only along the Z-axis, keeping X and Y fixed
            transform.position = new Vector3(transform.position.x, player.position.y + followDistance, player.position.z - followDistance);
        }
    }
}
