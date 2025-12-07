//THIS CODE WAS CLEANED-UP WITH AI


using Mirror;
using UnityEngine;

/// <summary>
/// Creates and manages a minimap camera for the local player only.
/// The server does not need the minimap; each client spawns its own.
/// </summary>
public class MiniMap : NetworkBehaviour
{
    [Header("Minimap Camera Settings")]
    [SerializeField] private GameObject miniMapCameraPrefab;  // Prefab of overhead camera
    [SerializeField] private Vector3 cameraOffset = new Vector3(0, 30f, 0); // Position above player
    [SerializeField] private bool rotateWithPlayer = false;   // Whether minimap rotates based on player's Y rotation

    private GameObject miniMapCamera;                         // The spawned minimap camera instance


    // ---------------------------------------------------------------------
    // Local Player Initialization
    // ---------------------------------------------------------------------

    /// <summary>
    /// Called when this object becomes the local player.
    /// Only the local player should create a minimap camera.
    /// </summary>
    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
        CreateMiniMapCamera();
    }


    // ---------------------------------------------------------------------
    // Minimap Camera Creation
    // ---------------------------------------------------------------------

    /// <summary>
    /// Instantiates the minimap camera prefab and attaches it to the player.
    /// </summary>
    private void CreateMiniMapCamera()
    {
        if (!miniMapCameraPrefab)
        {
            Debug.LogError("MiniMapCamera Prefab is not assigned in the inspector!");
            return;
        }

        // Create the minimap camera only for this client
        miniMapCamera = Instantiate(miniMapCameraPrefab);

        // Attach camera to the player so it follows the player automatically
        miniMapCamera.transform.SetParent(transform);
        miniMapCamera.transform.localPosition = cameraOffset;

        // Point straight downward for orthographic minimap view
        miniMapCamera.transform.localRotation = Quaternion.Euler(90f, 0f, 0f);

        // Ensure this client's minimap camera is active
        miniMapCamera.GetComponent<Camera>().enabled = true;
    }


    // ---------------------------------------------------------------------
    // Camera Rotation (Optional)
    // ---------------------------------------------------------------------

    private void Update()
    {
        // Allow minimap to rotate with player (common in top-down shooters)
        if (rotateWithPlayer && miniMapCamera != null)
        {
            miniMapCamera.transform.rotation = Quaternion.Euler(
                90f,
                transform.eulerAngles.y,
                0f
            );
        }
    }
}




//THIS CODE WAS CLEANED-UP WITH AI