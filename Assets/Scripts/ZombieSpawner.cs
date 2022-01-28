using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ZombieSpawner : MonoBehaviour
{
    [SerializeField] private List<WaveConfigSO> waves;
    [SerializeField] private List<Zombie> specialZombieList;
    [SerializeField] private List<Transform> specialSpawnPoints;

    public void SpawnEnemies()
    {
        foreach (var currentWave in waves)
        {
            StartCoroutine(currentWave.SpawnAllEnemiesInWave());
        }
    }
    public void SpawnSpecialZombie(int index)
    {
        if (specialZombieList.Count >= index)
        {
            Instantiate(specialZombieList[index], specialSpawnPoints[Random.Range(0, specialSpawnPoints.Count)]);
        }
    }
}
