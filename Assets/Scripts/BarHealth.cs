using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BarHealth : MonoBehaviour
{
    public int maxHp = 3;
    public int currentHp = 3;

    public void Damage(Zombie attacker)
    {
        currentHp -= 1;
        UpdateSkin();
        if (currentHp == 0)
        {
            Break(attacker);
            //LevelManager.Instance.DestroyFurniture(this.gameObject);
        }
    }

    public void Break(Zombie attacker)
    {
        this.tag = "Counter2";
        this.gameObject.GetComponent<Collider>().enabled = false;
        attacker.stopMovement = false;
    }

    public void Fix()
    {
        if (currentHp == maxHp)
            return;

        if (currentHp == 0)
            FixFromBroken();

        currentHp++;
        UpdateSkin();
    }

    private void UpdateSkin()
    {
        switch (currentHp)
        {
            case 0:
                // set broken sprite
            case 1:
                // set almost broken sprite
            case 2:
                // set slightly broken sprite
            case 3:
                // set normal sprite

                return;
        }
    }

    private void FixFromBroken()
    {
        this.tag = "Counter";
        this.gameObject.GetComponent<Collider>().enabled = true;
    }
}