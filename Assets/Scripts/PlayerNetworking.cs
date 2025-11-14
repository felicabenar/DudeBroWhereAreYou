using Mirror;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerNetworking : NetworkBehaviour
{

    //[SyncVar(hook = nameof(HitMessage))]
    //[SerializeField] int playerHealth = 100;
    [SerializeField] Color playerColor = Color.white;

    // Naming Stuff
    [SyncVar (hook = nameof(UpdateName))]
    [SerializeField] string playerName = "New Player";
    [SerializeField] Transform namePrefab, nameInstance;
    Vector3 nameOffset = new Vector3(0, .1f, 0);

    // UI Stuff

    void Awake()
    {
        nameInstance = Instantiate(namePrefab, transform.position + nameOffset, Quaternion.identity);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (isLocalPlayer)
        {
            // Listen to the onChangeName event, and link it to trigger the CmdUpdateName function
            NameChanger.onChangeName += CmdUpdateName; 
        }
        // If we are NOT the local player, disable the TankController 
            GetComponent<PlayerController>().enabled = isLocalPlayer;

        if (isLocalPlayer)
        {
            ChaseCamera.player = transform;
            foreach (Transform child in transform)
            {
                GetComponent<Renderer>().material.color = Color.green;
                child.GetComponent<Renderer>().material.color = Color.green;
            }

        }
        else
        {
            foreach (Transform child in transform)
            {
                GetComponent<Renderer>().material.color = Color.red;
                child.GetComponent<Renderer>().material.color = Color.red;
            }
        }
    }

    void LateUpdate()
    {
        nameInstance.position = transform.position + nameOffset;
        nameInstance.LookAt(Camera.main.transform);
        nameInstance.Rotate(0f, 180f, 0f); // Flip the label to face the player
    }

    /*void OnCollisionEnter(Collision collision)
    {
        if (!isServer) return;
        if (collision.gameObject.CompareTag("Bullet"))
        {
            CmdChangeHealth(-10);
        }
    }*/

    [Command]
    void CmdUpdateName(string newName)
    {
        playerName = newName;
    }

    void UpdateName(string oldName, string newName)
    {
        nameInstance.GetComponent<TMP_Text> ().text = newName;
    }

    /*[Command]
    void CmdChangeHealth(int damage)
    {
        playerHealth += damage;
    }

    void HitMessage(int oldHealth, int newHealth) {
        if (!isLocalPlayer) return;
        Debug.Log("You are the weakest link and have been voted off the island!");
    }*/
}
