using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BarHealth : MonoBehaviour
{
    public int maxHp = 3;
    public int currentHp = 3;

    public void Damage()
    {
        currentHp -= 1;
        
        if (currentHp == 0)
        {
            Break();
            LevelManager.Instance.DestroyFurniture(this.gameObject);
        }
    }

    public void Break()
    {
        
    }
}