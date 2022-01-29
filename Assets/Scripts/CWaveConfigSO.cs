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
    [SerializeField] int spawnPointVariation = 0;
    [SerializeField] List<RecipeSO> recipes = new List<RecipeSO>();
    [SerializeField] List<GameObject> customerPrefabs;
    [SerializeField] private List<GameObject> spawnPoints;

    public IEnumerator SpawnAllCustomersInWave()
    {
        int[] given = new int[] {0, 0, 0};
        
        while (recipesDifficulty[0] + recipesDifficulty[1] + recipesDifficulty[2] - given[0] - given[1] - given[2] > 0)
        {
            // Customer spawn
            int spawnPointIndex = Random.Range(0, spawnPointVariation);
            int customerIndex = Random.Range(0, customerPrefabs.Count);
            GameObject customer = Instantiate(customerPrefabs[customerIndex], spawnPoints[spawnPointIndex].transform);
            // Attach recipe
            int recipeDifficulty;
            do
            {
                recipeDifficulty = Random.Range(0, 2);
            } while (recipesDifficulty[recipeDifficulty] <= 0);

            var possibleRecipes = recipes.Where(r => r.difficulty == recipeDifficulty).Select(r => r).ToArray();
            customer.GetComponent<PreparingRecipe>().recipe = possibleRecipes[Random.Range(0, possibleRecipes.Length)];
            given[recipeDifficulty] -= 1;
            // Wait for next
            float waitTime = Random.Range(minSpawnDelay, maxSpawnDelay);
            yield return new WaitForSeconds(waitTime);
        }
        
        yield return new WaitForSeconds(0);
    }
	
}
