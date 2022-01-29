using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ZombieSpawner : MonoBehaviour
{
    public static ZombieSpawner Instance;
    //[Sepublic float distance;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }

        StartCoroutine( SpawnEnemies() );
    }
    
    [SerializeField] private List<WaveConfigSO> waves;
    [SerializeField] private List<Zombie> specialZombieList;
    [SerializeField] private List<Transform> spawnPoints;



    public IEnumerator SpawnEnemies()
    {
        foreach (var currentWave in waves)
        {
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
