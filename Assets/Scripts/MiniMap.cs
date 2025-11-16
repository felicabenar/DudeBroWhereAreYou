using Mirror;
using UnityEngine;

public class MiniMap : NetworkBehaviour
{

    [SerializeField] MiniMap miniMap;



    void Awake()
    {
        GetComponent<Renderer>();
    }

    void Start()
    {
        if (isLocalPlayer)
        {
            MiniMapCamera.player = transform;
        }
    }


}
