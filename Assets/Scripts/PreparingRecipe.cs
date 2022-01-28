using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Customer Config", fileName = "Customer wave config")]
public class PreparingRecipe : MonoBehaviour
{
    [SerializeField] public RecipeSO recipe;
    [SerializeField] private List<GameObject> fulfilledObjects;
    [SerializeField] public bool active = false;
}