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

    public IEnumerator ServerSpawnAllEnemiesInWave(List<Transform> spawnPoints, GameObject playerToFollow)
    {

        if (zombieCount == 0)
            Debug.LogWarning("zombieCount is 0 in wave. disable zombies");

        for (int i = 0; i < zombieCount; i++)
        {
            int spawnPointIndex = Random.Range(0, spawnPoints.Count);
            int zombieIndex = Random.Range(0, zombieVariationRange);
            ServerSpawn(zombiePrefabs[zombieIndex],spawnPoints[spawnPointIndex], playerToFollow);
            

            float waitTime = Random.Range(minSpawnDelay, maxSpawnDelay);
            yield return new WaitForSeconds(waitTime);
        }
        
        yield return new WaitForSeconds(delayForNext);
    }


    
    [Server]
    void ServerSpawn(GameObject zombiePrefab, Transform spawnPoint, GameObject playerToFollow)
    {
        GameObject zombie = Instantiate(zombiePrefab, spawnPoint.position, spawnPoint.rotation);
        zombie.GetComponent<Zombie>().player = playerToFollow.GetComponent<PlayerMovement>();
        NetworkServer.Spawn(zombie);
    }
   
}
