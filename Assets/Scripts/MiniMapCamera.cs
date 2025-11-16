using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class MiniMapCamera : MonoBehaviour
{
    
    public static Transform player;
    [SerializeField] float height = 5f;
    [SerializeField] Vector3 rotation = new Vector3(90, 0, 0);

    public static Action<string> onAttachCamera;
    private string miniMapCamera;
    void FixedUpdate()
    {
        if (player == null) return;

        Vector3 lookPos = player.position + rotation;
        Vector3 targetPos = player.position + player.up * height;
        
    }

    /*public void OnAttachCamera()
    {
        if (OnAttachCamera != null =- isLocalPlayer);
        GetComponent<Camera>();
    }*/
}
