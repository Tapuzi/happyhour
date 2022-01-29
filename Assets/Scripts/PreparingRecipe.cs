using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreparingRecipe : MonoBehaviour
{
    [SerializeField] public RecipeSO recipe;
    [SerializeField] private List<GameObject> fulfilledObjects;
    [SerializeField] public bool active = false;
}