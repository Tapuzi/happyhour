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


    [TargetRpc]
    public void TargetStartgame()
    {
        if (hasAuthority)
        {            
            StartCoroutine(SpawnOrders());
        }
           
    }

    public IEnumerator SpawnOrders()
    {
        foreach (var currentWave in waves)
        {
            Debug.Log("New orders wave incoming!");
            yield return currentWave.SpawnAllCustomersInWave(spawnPoints);
            //FIXME whait until zombie wabe finish
        }       
        //wait to next wave
    }


   















}
