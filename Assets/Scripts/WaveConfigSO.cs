using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Wave Config", fileName = "New Wave Config")]
public class WaveConfigSO : ScriptableObject
{
    [SerializeField] float maxSpawnDelay = 5f;
    [SerializeField] float minSpawnDelay = 1f;
    [SerializeField] int zombieCount = 5;
    [SerializeField] int zombieVariationRange = 0;
    [SerializeField] int spawnPointVariation = 0;
    

    [SerializeField] List<GameObject> zombiePrefabs;
    [SerializeField] private List<GameObject> spawnPoints;

    public IEnumerator SpawnAllEnemiesInWave()
    {
        for (int i = 0; i < zombieCount; i++)
        {
            int spawnPointIndex = Random.Range(0, spawnPointVariation);
            int zombieIndex = Random.Range(0, zombieVariationRange);
            Instantiate(zombiePrefabs[zombieIndex], spawnPoints[spawnPointIndex].transform);
            float waitTime = Random.Range(minSpawnDelay, maxSpawnDelay);
            yield return new WaitForSeconds(waitTime);
        }
        
        yield return new WaitForSeconds(0);
    }
}
