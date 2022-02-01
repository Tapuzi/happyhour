using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using Mirror;


public class OrderSpawner : NetworkBehaviour
{
    
    //[Sepublic float distance;

    
    [SerializeField] private List<CWaveConfigSO> waves;
    //[SerializeField] private List<Order> specialZombieList;
    [SerializeField] private List<GameObject> spawnPoints;

    [SerializeField] private ZombieSpawner zombieSpawner;

    [SerializeField] private int waveIndex;
    [SerializeField] private int numPlayer;


    [Server]
    public void holdZombieSpawner(ZombieSpawner zombieSpawner)
    {
        this.zombieSpawner = zombieSpawner;
    }


   [TargetRpc]
    public void TargetStartgame(int numPlayer)
    {        
        if (hasAuthority)
        {
            waveIndex = 0;           
            this.numPlayer = numPlayer;

            startWave();//order first
        }
           
    }


    public void startWave()
    {
        StartCoroutine(SpawnOrders());
    }


    public IEnumerator SpawnOrders()
    {

        if(waveIndex <= waves.Count)
        {
            Debug.Log("New orders wave incoming!");

            //whait until zombie wabe finish
            yield return waves[waveIndex].SpawnAllCustomersInWave(spawnPoints, numPlayer);
            waveIndex++;
        }
        else
        {
            Debug.Log("all orders waves done");
        }

        CmdZombiesWave();

    }


    [Command]
    void CmdZombiesWave()
    {
        zombieSpawner.ServerStartWave();
    }


   















}
