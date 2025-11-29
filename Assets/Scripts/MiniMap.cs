using Mirror;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class MiniMap : MonoBehaviour
{

    public static Transform player;
    [SerializeField] float height = 5f;
    [SerializeField] Vector3 rotation = new Vector3(90, 0, 0);
    [SerializeField] MiniMap miniMap;

    //public static Action<string> onAttachCamera;
    //private string miniMapCamera;

/*
    void Awake()
    {
        GetComponent<Renderer>();
        GetComponent<Camera>();
    }
    */

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
        
        if (isLocalPlayer){
        Component.GetComponent.GameObject.FindWithTag(SecondCamera);
        }
    }*/


}
