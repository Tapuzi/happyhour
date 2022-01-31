using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Recipe Config", fileName = "Recipe config")]
//unuse
public class RecipeSO : ScriptableObject
{
    [SerializeField] public uint difficulty = 0;
    [SerializeField] public List<GameObject> requiredObjects;
}