using UnityEngine;

public class MinimapController : MonoBehaviour
{
    public Transform playerTransform; // Reference to the player's transform
    public Vector3 offset = new Vector3(0, 100, 0); // Offset of the minimap camera from the player

    void LateUpdate()
    {
        // Update the minimap camera's position based on the player's position
        Vector3 newPosition = playerTransform.position + offset;
        newPosition.z = transform.position.z; // Keep the minimap camera's z position constant
        transform.position = newPosition;
    }
}
