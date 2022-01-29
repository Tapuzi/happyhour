using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine;

public class PreparingRecipe : Interactable
{
    [SerializeField] public RecipeSO myRecipe;
    [SerializeField] private List<GameObject> requiredObjects;
    [SerializeField] public List<GameObject> ingridientPlaceholders;
    [SerializeField] public bool active = false;


    public bool SetRecipe(RecipeSO recipe)
    {
        if (myRecipe == null)
        {
            myRecipe = recipe;
            requiredObjects = recipe.requiredObjects.ToList();
            RefreshPlaceholders();
            return true;
        }

        return false;
    }

    void RefreshPlaceholders()
    {
        for (int i = 0; i < requiredObjects.Count && i < ingridientPlaceholders.Count; i++)
        {
            if (ingridientPlaceholders[i].transform.childCount > 0)
                Destroy(ingridientPlaceholders[i].transform.GetChild(0).gameObject);
            if (requiredObjects[i] != null)
                Instantiate(requiredObjects[i], ingridientPlaceholders[i].transform);
        }
    }

    public override void Interact(PlayerInteract interact)
    {
        foreach (var requiredObject in requiredObjects.Where(ro => ro != null))
        {
            var heldItem = interact.GetHeldItem();
            // Remove item if possible
            if (heldItem != null && heldItem.CompareTag(requiredObject.tag))
            {
                // Delete the item from player's hand (null for placeholder freeze position)
                requiredObjects.Insert(requiredObjects.IndexOf(requiredObject), null);
                requiredObjects.Remove(requiredObject);
                RefreshPlaceholders();
                interact.RemoveItem();
                break;
            }
        }

        if (requiredObjects.Where(s => s != null).ToArray().Length == 0)
        {
            FinishRecipe();
        }
    }

    void FinishRecipe()
    {
        myRecipe = null;
        Debug.Log("Money +10");
        Destroy(gameObject);
    }
}