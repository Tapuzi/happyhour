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

    [HideInInspector]
    public GameObject holdPlayer;


    [Server]
    public void ServerStartGame()
    {
        StartCoroutine(ServerSpawnEnemies());
    }


    public IEnumerator ServerSpawnEnemies()
    {
        foreach (var currentWave in waves)
        {
            Debug.Log("New enemies wave incoming!");
            yield return currentWave.ServerSpawnAllEnemiesInWave(spawnPoints, holdPlayer);
        }

    }

     

    /*public void SpawnSpecialZombie(int index)
    {
        if (specialZombieList.Count >= index)
        {
            Instantiate(specialZombieList[index], spawnPoints[Random.Range(0, spawnPoints.Count)]);
        }
    }*/
}
