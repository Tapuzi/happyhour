using UnityEngine;

public class Bullet : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject,2f);
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Zombie"))
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }
}
