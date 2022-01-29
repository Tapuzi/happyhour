using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkPlayer : NetworkBehaviour
{
        [SyncVar] [SerializeField] private string displayName = "MissingName";
        [SyncVar(hook=nameof(HandleDisplayColorUpdated))] [SerializeField] private Color color = Color.black;
        [SerializeField] private int clientNum = -1;
        
        

        public List<MonoBehaviour> componetsToEnableInLocal;
        

    #region Server
        [Server]
        public void SetPlayerNum(int num)
        {
            clientNum = num;
        }

        

        [Server]
        public void SetDisplayName(string newDisplayName)
        {
                displayName = newDisplayName;
        }

        [Server]
        public void SetColor()
        {
                color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f),Random.Range(0f, 1f));
        }

        [Command]
        private void CmdSetDisplayName(string newDisplayName)
        {
            print("CmdSetDisplayName");
                RpcLogNewName(newDisplayName);
                
                SetDisplayName(newDisplayName);
        }


    
        /*[Command]
        void CmdNumMeID(uint netId)
        {
            //print("CmdNumMeID netId is "+netId +" PlayerNum is "+clientNum);
            RpcNumMeID(netId,clientNum);
        }*/

        [Command]
        void CmdStartGame(int t)
        {
            print("server indication game start");
            RpcStartGame(t);
        }

       

    #endregion

    #region Client

    public static NetworkPlayer localPlayer;

    public bool isGameStart = false;

    public override void OnStartClient()
    {

        /*if (isLocalPlayer){
            CmdNumMeID(netId);            
        }*/


        int t =  GameObject.FindGameObjectsWithTag("Player").Length;
        print("OnStartClient have "+t+" Player and isLocalPlayer " + isLocalPlayer);

        if(isLocalPlayer)
        {
            localPlayer = this;
            
            if(t == 2)
            {
                StartGame(t);
                CmdStartGame(t);
            }
        }
    }

    [ClientRpc]
    private void RpcStartGame(int t)
    {
        print("isLocalPlayer "+isLocalPlayer+" t "+t);
        if(!isLocalPlayer && t==2)
                
            localPlayer.StartGame(1);
        
    }

    private void StartGame(int t){
        print("RpcStartGame "+gameObject.name);
         if (t == 1)//first player
            {
                print("is player 1 start game");
                GameObject mapPlane = GameObject.FindWithTag("mapPlane1");
                Camera cam = GameObject.FindWithTag("camera1").GetComponent<Camera>();

                PlayerMovement movement = GetComponent<PlayerMovement>();
                movement.mapPlane = mapPlane;
                movement.cam = cam;

                Transform spawn = GameObject.FindWithTag("spawn1").transform;
                transform.position = spawn.position;

            }
        else if (t == 2)//second player
            {
                print("is player 2 start game");
                GameObject mapPlane = GameObject.FindWithTag("mapPlane2");
                Camera cam = GameObject.FindWithTag("camera2").GetComponent<Camera>();

                PlayerMovement movement = GetComponent<PlayerMovement>();
                movement.mapPlane = mapPlane;
                movement.cam = cam;

                Transform spawn = GameObject.FindWithTag("spawn2").transform;
                transform.position = spawn.position;
            }
        print("isGameStart true");
        isGameStart = true;
        foreach (var componets in componetsToEnableInLocal)
        {
                componets.enabled = true;
        }
    }

    private void HandleDisplayColorUpdated(Color oldColor, Color newColor)
        {
                //GetComponentInChildren<SpriteRenderer>().color = newColor;
        }

        [ClientRpc]
        private void RpcLogNewName(string newDisplayName)
        {
                Debug.Log(newDisplayName);
        }

 

        #endregion

        
}