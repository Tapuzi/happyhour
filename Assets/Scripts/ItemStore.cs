using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;
using Mirror;


public class ItemStore : Interactable {
    public List<GameObject> items = new List<GameObject>();
    private string[] itemTags;

    public void Start()
    {
        itemTags = items.Select(i => i.tag).ToArray();
    }

    public GameObject CycleItem(GameObject currentItem, GameObject spawnPosition)
    {
        // Determine item to spawn
        GameObject item;
        // If no item or an item from another container, get first
        if (currentItem == null || !itemTags.Contains(currentItem.tag))
        {
            item = items[0];
        }
        // Cycle through items
        else
        {
            int index = 0;
            while (!items[index].CompareTag(currentItem.tag) && index + 1 < items.Count)
                index += 1;
            if (index + 1 < items.Count)
                item = items[index + 1];
            else
                item = items[0];
        }
        // Decided what to spawn
      

        Transform parent = spawnPosition.transform.GetComponentInParent<NetworkIdentity>().GetComponent<Transform>();//NetworkIdentity must be parent

        print("parent is " + parent + " child is " + spawnPosition + " item is "+ item);

        NormalInstantiateLogic.instance.InstantiateNoWait(item, parent, spawnPosition.transform);
        GameObject spawn = NormalInstantiateLogic.instance.getLestGameObject();

        // spawn.transform.position = Vector3.zero;
        // Remove current item
        if (null != currentItem)
            Destroy(currentItem);
            // currentItem.SetActive(false);
        
        return spawn;
    }

    public override void Interact(PlayerInteract interact)
    {
        CycleItem(interact.GetHeldItem(), interact.itemParent);
    }
}