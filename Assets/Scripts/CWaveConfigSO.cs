using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using Mirror;

[CreateAssetMenu(menuName = "Customer wave config", fileName = "Customer wave config")]
public class CWaveConfigSO : ScriptableObject
{
    [SerializeField] float maxSpawnDelay = 7f;
    [SerializeField] float minSpawnDelay = 3f;
    //recipe difficulty is number items in recipe.
    [SerializeField] List<RecipeSO> recipes;
    [SerializeField] GameObject orderPrefab;


    private bool FlagsWaitForServer = true;
   

    public IEnumerator SpawnAllCustomersInWave(List<GameObject> spawnPoints)
    {

        //GameObject order = Instantiate(orderPrefab, spawnPoints[0].transform);
        //if (order.GetComponent<PreparingRecipe>().SetRecipe(recipes[0]))
           
     


        //deep copy pointers
        List<RecipeSO> recipesThisWave = recipes.Select(r => r).ToList();

        // While there are still recipes to spawn
        while (recipesThisWave.Count != 0)
        {
           
            // Customer spawn
            GameObject[] possibleSpawn = spawnPoints.Where(sp => sp.transform.childCount == 0).ToArray();//Spawn when not have order
 
            if (possibleSpawn.Length == 0)  
                break;
            int spawnPointIndex = Random.Range(0, possibleSpawn.Length);
           
            yield return NormalInstantiateLogic.instance.Instantiate(orderPrefab, possibleSpawn[spawnPointIndex].transform);
            GameObject order = NormalInstantiateLogic.instance.getLestGameObject();            

            // Attach recipe

            int recipesIndex = Random.Range(0, recipesThisWave.Count);
            RecipeSO recipeSO = recipesThisWave[recipesIndex];
            recipesThisWave.RemoveAt(recipesIndex);

            order.GetComponent<PreparingRecipe>().SetRecipe(recipeSO);


            // Wait for next
            float waitTime = Random.Range(minSpawnDelay, maxSpawnDelay);
            yield return new WaitForSeconds(waitTime);
        }
        
        
    }







}
