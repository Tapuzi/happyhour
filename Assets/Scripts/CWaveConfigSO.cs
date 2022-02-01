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


    //unique - tag name to now where all orders are done. all orders prefab destroy.
    public IEnumerator SpawnAllCustomersInWave(List<GameObject> spawnPoints,int numPlayer)
    {
        //deep copy pointers
        List<RecipeSO> recipesThisWave = recipes.Select(r => r).ToList();
        string tag = "order" + numPlayer;

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
            order.tag = tag;

            // Attach recipe

            int recipesIndex = Random.Range(0, recipesThisWave.Count);
            RecipeSO recipeSO = recipesThisWave[recipesIndex];
            recipesThisWave.RemoveAt(recipesIndex);

            yield return new WaitForSeconds(0.01f);//try fix null randomaly bug
            order.GetComponent<PreparingRecipe>().SetRecipe(recipeSO);


            // Wait for next order
            float waitTime = Random.Range(minSpawnDelay, maxSpawnDelay);
            yield return new WaitForSeconds(waitTime);
        }

        // Wait for next wave
        while (GameObject.FindGameObjectsWithTag(tag).Count() != 0)
            yield return new WaitForSeconds(minSpawnDelay);


    }







}
