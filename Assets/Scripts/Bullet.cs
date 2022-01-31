using UnityEngine;

public class Bullet : MonoBehaviour
{
    void Start()
    {
        //only determination make the Destroy in both clients.
        //network deley can make bugs.
        Destroy(gameObject,2f);
    }

    public void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Zombie"))
        {
            //only determination make the Destroy in both clients.
            //network deley can make bugs.
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }
}
