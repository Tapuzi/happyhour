using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Shooting : NetworkBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;

    private float bulletForce = 20f;

    public float fireRate = 0.5F;
    private float nextFire = 0.0F;

    // Update is called once per frame
    void Update()
    {
        if(isLocalPlayer)
            if(Input.GetButtonDown("Fire1") && !Input.GetKeyDown(KeyCode.LeftControl))
            {                
                if(!FindObjectOfType<PlayerMovement>().IsCrouching())
                    if (Input.GetButton("Fire1") && Time.time > nextFire)
                    {
                        nextFire = Time.time + fireRate;
                        CmdShoot();
                    }
            
            }
    } 

    [Command]
    void CmdShoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        NetworkServer.Spawn(bullet);  
        RpcForceBullet(bullet);     
    }

    [ClientRpc]
    public void RpcForceBullet(GameObject bullet)
    {
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.AddForce(firePoint.forward * bulletForce, ForceMode.Impulse);
    }
}
