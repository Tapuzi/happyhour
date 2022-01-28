using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    private PlayerMovement player;
    private Transform playerTransform;
    [SerializeField] private float speed = 4f;
    private Rigidbody rb;

    public bool stopMovement = false;
    
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (player != null)
        {
            playerTransform = player.GetTransform();
            FollowPlayer();
        }
        else
        {
            player = FindObjectOfType<PlayerMovement>();
        }

        
    }

    private void FollowPlayer()
    {
        if (playerTransform == null || stopMovement)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            return;
        }

        Vector3 pos = Vector3.MoveTowards(transform.position, playerTransform.position, speed * Time.fixedDeltaTime);
        rb.MovePosition(pos);
        
        transform.rotation = Quaternion.LookRotation(playerTransform.position - transform.position, Vector3.up);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(other.gameObject);
            LevelManager.Instance.GameOver();
        }

        
    }

    private IEnumerator OnCollisionStay(Collision other)
    {
        if (other.gameObject.CompareTag("Counter"))
        {
            Debug.Log(other.gameObject.name);
            
            stopMovement = true;
            Collider tmp = other.collider;
            yield return new WaitForSeconds(1);
            if (stopMovement && tmp.enabled)
            {
                // damage counter
                other.gameObject.GetComponent<BarHealth>().Damage(this);

            }
        }
    }
    
    
    
    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Counter") || other.gameObject.CompareTag("Counter2"))
        {
            stopMovement = false;
        }
    }
}
