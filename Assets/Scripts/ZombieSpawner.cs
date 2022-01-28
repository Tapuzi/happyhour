using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    [SerializeField] private List<WaveConfigSO> waves;

    public void SpawnEnemies()
    {
        foreach (var currentWave in waves)
        {
            StartCoroutine(currentWave.SpawnAllEnemiesInWave());
        }
        
        
    }
}
