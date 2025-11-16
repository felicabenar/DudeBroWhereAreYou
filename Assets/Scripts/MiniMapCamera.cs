using Mirror.Examples.AssignAuthority;
using UnityEngine;

public class MiniMapCamera : MonoBehaviour
{
    
    public static Transform player;
    //[SerializeField] float distance = 5f;
    [SerializeField] Vector3 rotation = new Vector3(90, 0, 0);
    void FixedUpdate()
    {
        //if (player == null && isLocalPlayer)return;

        Vector3 lookPos = player.position + rotation;
        
    }
}
