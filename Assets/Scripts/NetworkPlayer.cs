using Mirror;
using UnityEngine;

public class NetworkPlayer : NetworkBehaviour
{
        [SyncVar] [SerializeField] private string displayName = "MissingName";
        [SyncVar(hook=nameof(HandleDisplayColorUpdated))] [SerializeField] private Color color = Color.black;

        #region Server

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
                RpcLogNewName(newDisplayName);
                
                SetDisplayName(newDisplayName);
        }

        #endregion

        #region Client

        private void HandleDisplayColorUpdated(Color oldColor, Color newColor)
        {
                GetComponentInChildren<SpriteRenderer>().color = newColor;
        }

        [ClientRpc]
        private void RpcLogNewName(string newDisplayName)
        {
                Debug.Log(newDisplayName);
        }

        #endregion

        
}