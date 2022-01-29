using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "Customer wave config", fileName = "Customer wave config")]
public class CWaveConfigSO : ScriptableObject
{
    [SerializeField] float maxSpawnDelay = 5f;
    [SerializeField] float minSpawnDelay = 1f;
    [SerializeField] private int[] recipesDifficulty = new []{2, 1, 0};  // Easy, medium hard recipies
    [SerializeField] List<RecipeSO> recipes = new List<RecipeSO>();
    [SerializeField] GameObject orderPrefab;
    //[SerializeField] private List<GameObject> spawnPoints;

    public IEnumerator SpawnAllCustomersInWave(List<GameObject> spawnPoints)
    {
        int[] given = new int[] {0, 0, 0};
        Debug.Log("Spawning recipes: Started");
        // While there are still recipes to spawn
        while (recipesDifficulty.Sum() - given.Sum() > 0)
        {
            Debug.Log("Spawning recipes: Spawning new");
            // Customer spawn
            GameObject[] possibleSpawn = spawnPoints.Where(sp => sp.transform.childCount == 0).ToArray();
            Debug.Log(possibleSpawn.Length + " possible recipe spawn location");
            if (possibleSpawn.Length == 0)  
                break;
            int spawnPointIndex = Random.Range(0, possibleSpawn.Length);
            GameObject order = Instantiate(orderPrefab, possibleSpawn[spawnPointIndex].transform);
            // Attach recipe
            int recipeDifficulty;
            do
            {
                recipeDifficulty = Random.Range(0, 2);
            } while (recipesDifficulty[recipeDifficulty] <= 0);

            var possibleRecipes = recipes.Where(r => r.difficulty == recipeDifficulty).Select(r => r).ToArray();
            if (order.GetComponent<PreparingRecipe>()
                .SetRecipe(possibleRecipes[Random.Range(0, possibleRecipes.Length)]))
            {
                given[recipeDifficulty] -= 1;
            }
            // Wait for next
            float waitTime = Random.Range(minSpawnDelay, maxSpawnDelay);
            yield return new WaitForSeconds(waitTime);
        }
        
        yield return new WaitForSeconds(0);
    }
	
}
