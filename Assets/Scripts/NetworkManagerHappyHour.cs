using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class NetworkManagerHappyHour : NetworkManager
{
    public override void OnStartClient()
    {
        
    }

    public override void OnServerAddPlayer(NetworkConnection conn)
    {
        base.OnServerAddPlayer(conn);

        NetworkPlayer player = conn.identity.GetComponent<NetworkPlayer>();
        
        player.SetDisplayName($"Player {numPlayers}");
        //player.SetPlayerNum(numPlayers);
        player.SetColor();

        //FindObjectOfType<ZombieSpawner>().SpawnEnemies();
    }
}
