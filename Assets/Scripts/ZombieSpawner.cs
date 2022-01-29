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

    public bool flag = true;


    
    [SerializeField] private List<WaveConfigSO> waves;
    [SerializeField] private List<Zombie> specialZombieList;
    [SerializeField] private List<Transform> spawnPoints;

    void Update(){
        if(NetworkPlayer.localPlayer != null)
            if(NetworkPlayer.localPlayer.isGameStart)
                if(flag)
                {
                    flag = false;      
                    if(isServer)
                    {              
                        print("start spwan");
                        StartCoroutine( SpawnEnemies() );
                    }
                }
    }

    public IEnumerator SpawnEnemies()
    {
        foreach (var currentWave in waves)
        {
            Debug.Log("New enemies wave incoming!");
            yield return currentWave.SpawnAllEnemiesInWave(spawnPoints);
        }

    }

     

    public void SpawnSpecialZombie(int index)
    {
        if (specialZombieList.Count >= index)
        {
            Instantiate(specialZombieList[index], spawnPoints[Random.Range(0, spawnPoints.Count)]);
        }
    }
}
