using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

[CreateAssetMenu(menuName = "Wave Config", fileName = "New Wave Config")]
public class WaveConfigSO : ScriptableObject
{
    [SerializeField] float maxSpawnDelay = 5f;
    [SerializeField] float minSpawnDelay = 1f;
    [SerializeField] int zombieCount = 5;
    [SerializeField] int zombieVariationRange = 0;
    [SerializeField] int spawnPointVariation = 10;
    

    [SerializeField] List<GameObject> zombiePrefabs;
    //[SerializeField] List<Transform> spawnPoints;

    [SerializeField] int delayForNext = 10;

    public IEnumerator SpawnAllEnemiesInWave(List<Transform> spawnPoints)
    {
        for (int i = 0; i < zombieCount; i++)
        {
            int spawnPointIndex = Random.Range(0, spawnPoints.Count);
            int zombieIndex = Random.Range(0, zombieVariationRange);
            //CmdSpawn(zombiePrefabs[zombieIndex],spawnPoints[spawnPointIndex]);            
            GameObject zombie = Instantiate(zombiePrefabs[zombieIndex]);
            zombie.transform.position = spawnPoints[spawnPointIndex].transform.position;
            Debug.Log("spawn: " + spawnPoints[spawnPointIndex]);
            NetworkServer.Spawn(zombie);

            float waitTime = Random.Range(minSpawnDelay, maxSpawnDelay);
            yield return new WaitForSeconds(waitTime);
        }
        
        yield return new WaitForSeconds(delayForNext);
    }


    /*[Command]
    void CmdSpawn(GameObject a,Transform b)
    {
        GameObject zombie = Instantiate(a, b);
        NetworkServer.Spawn(zombie);
    }*/
   
}
