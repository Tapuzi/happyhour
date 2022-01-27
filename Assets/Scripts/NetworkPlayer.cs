using Mirror;
using UnityEngine;

public class NetworkPlayer : NetworkBehaviour
{
        [SyncVar] [SerializeField] private string displayName = "MissingName";
        [SyncVar] [SerializeField] private Color color;

        [Server]
        public void SetDisplayName(string newDisplayName)
        {
                displayName = newDisplayName;
        }

        [Server]
        public void SetColor()
        {
                color = new Color(Random.Range(0, 1), Random.Range(0, 1),Random.Range(0, 1));
        }
}