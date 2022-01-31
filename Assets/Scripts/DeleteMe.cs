using System.Collections;
using UnityEngine;
using Mirror;


public class DeleteMe : NetworkBehaviour
{



    /*public override void OnStartAuthority()
    {
        StartCoroutine(destroy(2f));
    }*/


    public override void OnStartServer()
    {
        StartCoroutine(destroy(2f));

    }



    private IEnumerator destroy(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        NetworkServer.Destroy(gameObject);        
    }






    // Update is called once per frame
    void Update()
    {
        /*if(hasAuthority) //in network NetworkTransform check clientAuthority.
        {
            transform.position = transform.position + Vector3.forward * Time.deltaTime;           
        }*/


        if (isServer)//in network NetworkTransform uncheck clientAuthority.
        {
            transform.position = transform.position + Vector3.forward * Time.deltaTime;        
        }

        
    }
}
