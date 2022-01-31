using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkPlayer : NetworkBehaviour
{
    public List<MonoBehaviour> componetsToEnableInLocal;


    void Start()
    {
        
    }


    [TargetRpc]
    public void TargetStartGame(NetworkConnection target,int playerNum) {
        print("RpcStartGame isLocalPlayer" + isLocalPlayer);

        if (playerNum == 0)//first player
        {
            print("is player 1 start game");
            GameObject mapPlane = GameObject.FindWithTag("mapPlane1");
            Camera cam = GameObject.FindWithTag("camera1").GetComponent<Camera>();

            PlayerMovement movement = GetComponent<PlayerMovement>();
            movement.mapPlane = mapPlane;
            movement.cam = cam;

            Transform spawn = GameObject.FindWithTag("spawn1").GetComponent<Transform>();
            transform.position = spawn.position;

        }
        else if (playerNum == 1)//second player
        {
            print("is player 2 start game");
            GameObject mapPlane = GameObject.FindWithTag("mapPlane2");
            Camera cam = GameObject.FindWithTag("camera2").GetComponent<Camera>();

            PlayerMovement movement = GetComponent<PlayerMovement>();
            movement.mapPlane = mapPlane;
            movement.cam = cam;

            Transform spawn = GameObject.FindWithTag("spawn2").GetComponent<Transform>();
            transform.position = spawn.position;
        }        

        foreach (var componets in componetsToEnableInLocal)
        {
            componets.enabled = true;
        }
    }




}