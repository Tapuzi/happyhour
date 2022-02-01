using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;


//Fake logic of "GameObject order = Instantiate(GameObject original, Transform parent)"
//use "yield return NormalInstantiateLogic.instance.Instantiate(GameObject original, Transform parent)" insted
//and GameObject order = NormalInstantiateLogic.instance.getLestGameObject()
public class NormalInstantiateLogic : NetworkBehaviour
{
    public static NormalInstantiateLogic instance;


    //move to server the index
    public List<GameObject> prefabsList;
    public float timeWait = 0.05f;

    private GameObject lestGameObject;



    public override void OnStartAuthority()
    {

        lestGameObject = null;


        //only Authority can acsse.
        instance = this;
    }


    //not wait until server return the objects
    //parent is networidentify
    public void InstantiateNoWait(GameObject original, Transform parent, Transform child)
    {           
        StartCoroutine(Instantiate(original, parent, child.name));
    }

    public IEnumerator Instantiate(GameObject original, Transform parent)
    {
        yield return Instantiate(original, parent, null);
    }

    public IEnumerator Instantiate(GameObject original, Transform parent,string childName)
    {

        //find original in list
        int prefabIndex = -1;
        for (int i=0;i< prefabsList.Count; i++)
        {
            if (prefabsList[i] == original)
            {
                prefabIndex = i;
                break;
            }
                
        }

        if(prefabIndex == -1)
        {
            Debug.LogError("this  prefab not in list. go to playerIntegration prefab and add it");
        }
        else
        {
            lestGameObject = null;
            CmdSpawn(prefabIndex, parent, childName);
            while (lestGameObject == null)
                yield return new WaitForSeconds(timeWait);
        }        
    }

    public GameObject getLestGameObject()
    {
        return lestGameObject;
    }


    [Command]
    void CmdSpawn(int prefabIndex, Transform parent, string childName)
    {
        GameObject original = prefabsList[prefabIndex];       

        GameObject newObject = NetworkBehaviour.Instantiate(original);
        NetworkServer.Spawn(newObject, gameObject);
        TargetSpawn(newObject);
        RpcSetParent(newObject,parent, childName);

    }

    [ClientRpc]
    void RpcSetParent(GameObject newObject,Transform parent, string childName)
    {
        if (childName == null)
            newObject.transform.SetParent(parent, false);
        else
        {            
            Transform child = parent.transform.Find(childName);
            newObject.transform.SetParent(child,false);            
        }
            


    }

    [TargetRpc]
    void TargetSpawn(GameObject newObject)
    {        
        lestGameObject = newObject;
    }
}
