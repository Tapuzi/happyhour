using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;


public class BarHealth : MonoBehaviour
{
    public int maxHp = 3;
    public int currentHp = 3;
    [SerializeField] List<GameObject> skins;
    [SerializeField] private float damageTick = 1.5f;
    bool flag = true;
    
    
    
    public void Damage(Zombie attacker)
    {
        if (!flag)
            return;

        flag = false;
        
        StartCoroutine(ProtectedForSeconds(attacker));
    }

    private IEnumerator ProtectedForSeconds(Zombie attacker)
    {
        yield return new WaitForSeconds(damageTick);
        flag = true;
        currentHp -= 1;
        UpdateSkin(currentHp);
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
        UpdateSkin(currentHp);
    }

    private void UpdateSkin(int skinIndex)
    {
        for(int i = 0; i < skins.Count; i++)
        {
            if(i == skinIndex)
                skins[i].SetActive(true);
            else
            {
                skins[i].SetActive(false);
            }
        }
    }

    private void FixFromBroken()
    {
        this.tag = "Counter";
        this.gameObject.GetComponent<Collider>().enabled = true;
    }
}