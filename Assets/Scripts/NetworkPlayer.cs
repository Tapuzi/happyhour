using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkPlayer : NetworkBehaviour
{
        [SyncVar] [SerializeField] private string displayName = "MissingName";
        [SyncVar(hook=nameof(HandleDisplayColorUpdated))] [SerializeField] private Color color = Color.black;
        [SyncVar] [SerializeField] private int PlayerNum;
        
        public List<MonoBehaviour> componetsToEnableInLocal;
        

    #region Server

        [Server]
        public void SetDisplayName(string newDisplayName)
        {
                displayName = newDisplayName;
        }

        [Server]
        public void SetPlayerNum(int num)
        {
            PlayerNum = num;
        }

        [Server]
        public void SetColor()
        {
                color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f),Random.Range(0f, 1f));
        }

        [Command]
        private void CmdSetDisplayName(string newDisplayName)
        {
                RpcLogNewName(newDisplayName);
                
                SetDisplayName(newDisplayName);
        }


    

    #endregion

    #region Client



    public override void OnStartClient()
    {
        /*if (isLocalPlayer)
        {

            print("netId is "+netId);
            if (netId == 1)//first player
            {
                print("is player 1");
                GameObject mapPlane = GameObject.FindWithTag("mapPlane1");
                Camera cam = GameObject.FindWithTag("camera1").GetComponent<Camera>();

                PlayerMovement movement = GetComponent<PlayerMovement>();
                movement.mapPlane = mapPlane;
                movement.cam = cam;

                Transform spawn = GameObject.FindWithTag("spawn1").transform;
                transform.position = spawn.position;

            }
            else if (netId == 2)//second player
            {
                print("is player 2");
                GameObject mapPlane = GameObject.FindWithTag("mapPlane2");
                Camera cam = GameObject.FindWithTag("camera2").GetComponent<Camera>();

                PlayerMovement movement = GetComponent<PlayerMovement>();
                movement.mapPlane = mapPlane;
                movement.cam = cam;

                Transform spawn = GameObject.FindWithTag("spawn2").transform;
                transform.position = spawn.position;
            }
            else
                Debug.LogError("only 2 players!!!!!");


            foreach (var componets in componetsToEnableInLocal)
            {
                componets.enabled = true;
            }
        }*/

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