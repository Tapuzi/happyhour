using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using Mirror;


public class BarHealth : NetworkBehaviour
{
    public int maxHp = 3;
    public int currentHp = 3;
    [SerializeField] List<GameObject> skins;
    [SerializeField] private float damageTick = 1.5f;
    bool flag = true;
    
    
    [Server]
    public void ServerDamage(Zombie attacker)
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
        RpcUpdateSkin(currentHp);
        if (currentHp == 0)
        {
            RpcBreak();
            attacker.stopMovement = false;
            //LevelManager.Instance.DestroyFurniture(this.gameObject);
        }
    }

    [ClientRpc]
    public void RpcBreak()
    {
        this.tag = "Counter2";
        this.gameObject.GetComponent<Collider>().enabled = false;
        
    }

    public void Fix()
    {
        if (currentHp == maxHp)
            return;

        if (currentHp == 0)
            FixFromBroken();

        currentHp++;
        RpcUpdateSkin(currentHp);
    }

    [ClientRpc]
    private void RpcUpdateSkin(int skinIndex)
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