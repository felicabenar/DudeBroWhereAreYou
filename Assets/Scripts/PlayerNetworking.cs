//THIS CODE WAS CLEANED-UP WITH AI 



using Mirror;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerNetworking : NetworkBehaviour
{
    // -------------------------------
    // Player Name Syncing
    // -------------------------------

    // SyncVar: synchronized across network.
    // When "playerName" changes on the server, all clients automatically run UpdateName().
    [SyncVar(hook = nameof(UpdateName))]
    [SerializeField] string playerName = "New Player";

    // Prefab and instance for name label above the player's head
    [SerializeField] Transform namePrefab;
    [SerializeField] Transform nameInstance;

    // Offset for positioning name label above the player
    Vector3 nameOffset = new Vector3(0, .1f, 0);


    // -------------------------------
    // Initialization
    // -------------------------------

    private void Awake()
    {
        // Spawn the floating name label UI
        nameInstance = Instantiate(namePrefab, transform.position + nameOffset, Quaternion.identity);

        // Reads stored player preferences (TransportValue), though not used here
        PlayerPrefs.GetInt("TransportValue");
    }

    private void Start()
    {
        if (isLocalPlayer)
        {
            // Local player listens for the global name change event
            // When fired, calls CmdUpdateName (runs on server)
            NameChanger.onChangeName += CmdUpdateName;
        }

        // Disable PlayerController on remote players
        GetComponent<PlayerController>().enabled = isLocalPlayer;
    }


    // -------------------------------
    // Update Name UI Position + Facing
    // -------------------------------

    private void LateUpdate()
    {
        // Follow player position
        nameInstance.position = transform.position + nameOffset;

        // Make the label face the main camera
        nameInstance.LookAt(Camera.main.transform);

        // Rotate 180º so the text appears upright to the camera
        nameInstance.Rotate(0f, 180f, 0f);
    }


    // -------------------------------
    // Commands & SyncVar Hooks
    // -------------------------------

    // Command: Runs on the server when called from a client
    [Command]
    private void CmdUpdateName(string newName)
    {
        playerName = newName; // Triggers SyncVar hook on all clients
    }

    // SyncVar hook: called on clients when "playerName" changes
    private void UpdateName(string oldName, string newName)
    {
        nameInstance.GetComponent<TMP_Text>().text = newName;
    }
}




//THIS CODE WAS CLEANED-UP WITH AI 