using UnityEngine;
using Mirror;

public class TriggerBoxCollision : MonoBehaviour
{
    [SerializeField] private string message = "You are here";  
    [SerializeField] private bool onlyOnce = true;             // Prevent repeated triggers if needed

    private bool hasTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        // Ignore non-player objects
        if (!other.TryGetComponent<NetworkIdentity>(out var identity))
            return;

        // Only once?
        if (onlyOnce && hasTriggered)
            return;

        hasTriggered = true;

        // If this is a player object that belongs to the local client → show message
        if (identity.isLocalPlayer)
        {
            Debug.Log(message);
        }
    }
}


/*
void OnCollisionEnter(Collision collision)
    {
        Transform tform = collision.transform;
        if (tform.GetComponent<PlayerController>())
        {
            PlayerController tc = tform.GetComponent<PlayerController>();
            tform.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
            tform.position = tc.startPos;
            tform.localEulerAngles = tc.startRot;
        }       
    }
*/