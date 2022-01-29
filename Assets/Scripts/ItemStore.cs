using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;

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
        GameObject spawn = Instantiate(item, spawnPosition.transform.position, spawnPosition.transform.rotation, spawnPosition.transform);
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