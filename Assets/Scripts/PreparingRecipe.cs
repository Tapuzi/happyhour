using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PreparingRecipe : Interactable
{
    [SerializeField] public RecipeSO recipe;
    [SerializeField] private List<GameObject> requiredObjects;
    [SerializeField] public bool active = false;


    public void SetRecipe(RecipeSO recipe)
    {
        requiredObjects = recipe.requiredObjects.ToList();
    }
    

    public override void Interact(PlayerInteract interact)
    {
        foreach (var requiredObject in requiredObjects)
        {
            if (interact.GetHeldItem().CompareTag(requiredObject.tag))
            {
                requiredObjects.Remove(requiredObject);
                // Delete the item from player's hand
                break;
            }
        }

        if (requiredObjects.Count == 0)
        {
            Debug.Log("Money +10");
        }
    }
}