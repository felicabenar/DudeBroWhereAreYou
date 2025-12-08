//THIS CODE WAS CLEANED-UP WITH AI

using UnityEngine;

/// <summary>
/// Smooth chase camera that follows a target player from behind.
/// </summary>
public class ChaseCamera : MonoBehaviour
{
    /// <summary> The player transform the camera will follow. </summary>
    public static Transform player;

    [Header("Follow Settings")]
    [SerializeField] private float distance = 1f;       // Distance behind the player
    [SerializeField] private float height = 1f;         // Height above the player
    [SerializeField] private Vector3 offset = new Vector3(0, 1, 0);     // Extra offset for aiming at the player

    [Header("Smoothing Settings")]
    [SerializeField] private float moveSpeed = 10f;     // How fast the camera moves
    [SerializeField] private float rotSpeed = 5f;      // How fast the camera rotates

    private void FixedUpdate()
    {
        if (player == null) return;

        // --- ROTATION ---
        // Position to look at (usually above the player's head)
        Vector3 lookPos = player.position + offset;

        // Smoothly rotate the camera to face the player
        Quaternion targetRot = Quaternion.LookRotation(lookPos - transform.position);
        transform.rotation = Quaternion.Lerp(
            transform.rotation,
            targetRot,
            rotSpeed * Time.fixedDeltaTime
        );

        // --- POSITION ---
        // Calculate follow position behind and above the player
        Vector3 targetPos = player.position
                            + player.up * height
                            - player.forward * distance;

        // Smoothly move camera toward the follow position
        transform.position = Vector3.Lerp(
            transform.position,
            targetPos,
            moveSpeed * Time.fixedDeltaTime
        );
    }
}


//THIS CODE WAS CLEANED-UP WITH AI