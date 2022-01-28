using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ZombieSpawner : MonoBehaviour
{
    public static ZombieSpawner Instance;

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
    }
    
    [SerializeField] private List<WaveConfigSO> waves;
    [SerializeField] private List<Zombie> specialZombieList;
    [SerializeField] private List<Transform> spawnPoints;

    public void SpawnEnemies()
    {
        foreach (var currentWave in waves)
        {
            StartCoroutine(currentWave.SpawnAllEnemiesInWave(spawnPoints));
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
