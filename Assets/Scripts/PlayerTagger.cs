//THIS CODE WAS CLEANED-UP WITH AI 



using UnityEngine;
using Mirror;

public class PlayerTagger : NetworkBehaviour
{
    [SerializeField] private float tagRange = 2.5f;    // Distance within which another player can be tagged
    [SerializeField] private KeyCode tagKey = KeyCode.Q; // Key used to trigger a tag action
    [SerializeField] private LayerMask playerLayer;      // Layer mask used to detect players in the scene

    // Called only for the local player when they spawn
    public override void OnStartLocalPlayer()
    {
        // Assign local player's transform to the chase camera
        ChaseCamera.player = transform;
    }

    void Update()
    {
        // Only the local player should read keyboard input
        if (!isLocalPlayer) return;

        // Look for any nearby player within tag range
        PlayerTagger target = FindNearbyPlayer();

        // If a target was found and the player pressed the tag key
        if (target != null && Input.GetKeyDown(tagKey))
        {
            // Ask the server to process the tag, providing the target player's network ID
            CmdTagPlayer(target.netIdentity.netId);
        }
    }

    // Searches for a nearby player using OverlapSphere
    PlayerTagger FindNearbyPlayer()
    {
        // Detect all colliders within tag range that match the player layer
        Collider[] colliders = Physics.OverlapSphere(transform.position, tagRange, playerLayer);

        foreach (Collider col in colliders)
        {
            // Try to get a PlayerTagger component from the collider
            PlayerTagger player = col.GetComponent<PlayerTagger>();

            // If found and it's not this player, return it as a target
            if (player != null && player != this)
            {
                return player;
            }
        }

        // No valid player found
        return null;
    }

    // Command runs on the server — validates and processes the tag
    [Command]
    void CmdTagPlayer(uint targetNetId)
    {
        // Inform the GameTimer that a player has been tagged
        GameTimer.Instance.PlayerTagged();
    }
}



//THIS CODE WAS CLEANED-UP WITH AI 

