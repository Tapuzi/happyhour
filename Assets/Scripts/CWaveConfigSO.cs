using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "Customer wave config", fileName = "Customer wave config")]
public class CWaveConfigSO : ScriptableObject
{
    [SerializeField] float maxSpawnDelay = 7f;
    [SerializeField] float minSpawnDelay = 3f;
    //recipe difficulty is number items in recipe.
    [SerializeField] public int minItemsInOrder = 2;
    [SerializeField] public int maxItemsInOrder = 2;
    [SerializeField] public int numOrders = 5;
    [SerializeField] GameObject orderPrefab;

    public IEnumerator SpawnAllCustomersInWave(List<GameObject> spawnPoints)
    {

        //GameObject order = Instantiate(orderPrefab, spawnPoints[0].transform);
        //if (order.GetComponent<PreparingRecipe>().SetRecipe(recipes[0]))
    

        int[] given = new int[] {0, 0, 0};
        Debug.Log("Spawning recipes: Started");
        // While there are still recipes to spawn
        for (int i=0; i < numOrders;i++)
        {
            Debug.Log("Spawning recipes: Spawning new");
            // Customer spawn
            GameObject[] possibleSpawn = spawnPoints.Where(sp => sp.transform.childCount == 0).ToArray();//Spawn when not have order
            Debug.Log(possibleSpawn.Length + " possible recipe spawn location");
            if (possibleSpawn.Length == 0)  
                break;
            int spawnPointIndex = Random.Range(0, possibleSpawn.Length);

            GameObject order = Instantiate(orderPrefab, possibleSpawn[spawnPointIndex].transform);
            // Attach recipe

            order.GetComponent<PreparingRecipe>().SetRecipe(Random.Range(minItemsInOrder, maxItemsInOrder));


            // Wait for next
            float waitTime = Random.Range(minSpawnDelay, maxSpawnDelay);
            yield return new WaitForSeconds(waitTime);
        }
        
        yield return new WaitForSeconds(0);
    }
	
}
