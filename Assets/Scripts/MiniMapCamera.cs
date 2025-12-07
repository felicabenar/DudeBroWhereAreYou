//THIS CODE WAS CLEANED-UP WITH AI


using UnityEngine;

/// <summary>
/// Simple minimap camera controller.
/// Follows the player's X/Z position while staying at a fixed height,
/// and keeps a top-down rotation.
/// </summary>
public class MiniMapCamera : MonoBehaviour
{
    [Header("Target Player")]
    [SerializeField] private GameObject player;   // The player this minimap camera follows

    [Header("Camera Settings")]
    [SerializeField] private float height = 40f;  // Height above player
    [SerializeField] private Vector3 topDownRotation = new Vector3(90f, 0f, 0f); // Look straight down

    private void Start()
    {
        // Ensure camera is oriented properly on start
        transform.rotation = Quaternion.Euler(topDownRotation);
    }

    private void FixedUpdate()
    {
        if (!player) return;

        // Follow player's X/Z but keep constant height
        Vector3 pos = player.transform.position;
        transform.position = new Vector3(pos.x, height, pos.z);
    }
}



//THIS CODE WAS CLEANED-UP WITH AI