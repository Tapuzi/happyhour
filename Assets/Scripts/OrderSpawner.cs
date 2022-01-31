using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class OrderSpawner : MonoBehaviour
{
    //public static OrderSpawner Instance;
    //[Sepublic float distance;

    void Awake()
    {
        /*if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }*/

        StartCoroutine (SpawnOrders());
    }
    
    [SerializeField] private List<CWaveConfigSO> waves;
    //[SerializeField] private List<Order> specialZombieList;
    [SerializeField] private List<GameObject> spawnPoints;

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
    public void SpawnSpecialZombie(int index)
    {
        //if (specialZombieList.Count >= index)
        //{
        //    Instantiate(specialZombieList[index], spawnPoints[Random.Range(0, spawnPoints.Count)]);
        //}
    }
}
