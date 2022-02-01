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

    public OrderSpawner orderSpawner0;
    public OrderSpawner orderSpawner1;

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
            NetworkConnection conn0 = networkIdentity.connectionToClient;

            networkPlayer.TargetStartGame(networkIdentity.connectionToClient, 0);

            zombieSpawner0.holdPlayer = player;
            
            //second player
            player = players[1];
            networkPlayer = player.GetComponent<NetworkPlayer>();
            networkIdentity = player.GetComponent<NetworkIdentity>();
            NetworkConnection conn1 = networkIdentity.connectionToClient;

            networkPlayer.TargetStartGame(networkIdentity.connectionToClient, 1);

            zombieSpawner1.holdPlayer = player;

            //zombies
            zombieSpawner0.GetComponent<NetworkIdentity>().AssignClientAuthority(conn0);
            zombieSpawner1.GetComponent<NetworkIdentity>().AssignClientAuthority(conn1);

            zombieSpawner0.RpcHoldOrderSpawner(orderSpawner0);
            zombieSpawner1.RpcHoldOrderSpawner(orderSpawner1);

            zombieSpawner0.ServerStartGame();
            zombieSpawner1.ServerStartGame();
            
            //order
            orderSpawner0.GetComponent<NetworkIdentity>().AssignClientAuthority(conn0);
            orderSpawner1.GetComponent<NetworkIdentity>().AssignClientAuthority(conn1);


            orderSpawner0.holdZombieSpawner(zombieSpawner0);
            orderSpawner1.holdZombieSpawner(zombieSpawner1);


            //TODO https://mirror-networking.gitbook.io/docs/guides/authority add authority to player 0          
           
            orderSpawner0.TargetStartgame(0);                        
            orderSpawner1.TargetStartgame(1);
        }
        



        
    }
}
