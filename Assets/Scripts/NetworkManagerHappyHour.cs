using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class NetworkManagerHappyHour : NetworkManager
{
    /*public override void OnStartClient()
    {
        
    }*/


    public ZombieSpawner zombieSpawner0;
    public ZombieSpawner zombieSpawner1;

    public override void OnServerAddPlayer(NetworkConnection conn)
    {
        print("OnServerAddPlayer ");
        base.OnServerAddPlayer(conn);




        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        if(players.Length == 2)
        {
            //first player
            GameObject player = players[0];
            NetworkPlayer networkPlayer = player.GetComponent<NetworkPlayer>();
            NetworkIdentity networkIdentity = player.GetComponent<NetworkIdentity>();

            networkPlayer.TargetStartGame(networkIdentity.connectionToClient, 0);

            zombieSpawner0.holdPlayer = player;
            
            //second player
            player = players[1];
            networkPlayer = player.GetComponent<NetworkPlayer>();
            networkIdentity = player.GetComponent<NetworkIdentity>();

            networkPlayer.TargetStartGame(networkIdentity.connectionToClient, 1);

            zombieSpawner1.holdPlayer = player;

            //zombies
            zombieSpawner0.ServerStartGame();
            zombieSpawner1.ServerStartGame();
        }
        



        
    }
}
