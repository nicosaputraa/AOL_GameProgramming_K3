using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player; // Reference to the player's transform

    void Update()
    {
        if (player != null)
        {
            // Set the camera's position to follow the player, keeping Z position constant for 2D
            transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);
        }
    }
}
