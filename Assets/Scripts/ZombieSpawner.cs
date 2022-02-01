using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using Mirror;

public class ZombieSpawner : NetworkBehaviour
{
    //public static ZombieSpawner Instance
    //[Sepublic float distance;


    
    [SerializeField] private List<WaveConfigSO> waves;
    [SerializeField] private List<Zombie> specialZombieList;
    [SerializeField] private List<Transform> spawnPoints;

    [SerializeField] private OrderSpawner orderSpawner;
    [SerializeField] private int waveIndex;

    [HideInInspector]public GameObject holdPlayer;


    [Server]
    public void ServerStartGame()
    {

        waveIndex = 0;
    }

    [TargetRpc]
    public void RpcHoldOrderSpawner(OrderSpawner orderSpawner)
    {
        this.orderSpawner = orderSpawner;
    }


    [Server]
    public void ServerStartWave()
    {
        StartCoroutine(ServerSpawnEnemies());
    }


    public IEnumerator ServerSpawnEnemies()
    {



        if (waveIndex <= waves.Count)
        {
            Debug.Log("New enemies wave incoming!");

            //whait until zombie wabe finish
            yield return waves[waveIndex].ServerSpawnAllEnemiesInWave(spawnPoints, holdPlayer);
            waveIndex++;
        }
        else
        {
            Debug.Log("all enemies waves done");
        }

        TargetOrderWave();

    }

    [TargetRpc]
    void TargetOrderWave()
    {
        orderSpawner.startWave();
    }

}
