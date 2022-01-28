using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    public static List<GameObject> Furniture = new List<GameObject>();

    public int TotalHealth
    {
        get
        {
            return Furniture.Select(f => f.GetComponent<BarHealth>().currentHp).ToArray().Sum();
        }
    }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public void GameOver()
    {
        Debug.Log("Game Over");
    }

    public void DestroyFurniture(GameObject furniture)
    {
        Furniture.Remove(furniture);
    }
}
