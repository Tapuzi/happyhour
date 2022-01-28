using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    private PlayerMovement player;
    private Transform playerTransform;
    [SerializeField] private float speed = 4f;
    private Rigidbody2D rb;

    private bool stopMovement = false;
    
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
        rb = GetComponent<Rigidbody2D>();
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
            return;
        }

        Vector3 pos = Vector3.MoveTowards(transform.position, playerTransform.position, speed * Time.fixedDeltaTime);
        rb.MovePosition(pos);

        Vector3 perpendicular = Vector3.Cross(transform.position - playerTransform.position,Vector3.forward);
        transform.rotation = Quaternion.LookRotation(Vector3.forward, perpendicular);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(other.gameObject);
            LevelManager.Instance.GameOver();
        }

        
    }

    private IEnumerator OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Counter"))
        {
            stopMovement = true;
            yield return new WaitForSeconds(2);
            if (stopMovement)
            {
                var barHealthItem = other.gameObject.GetComponent<BarHealth>();
                if (barHealthItem == null)
                    Debug.Log("Help");
                barHealthItem.currentHp -= 1;
                if (barHealthItem.currentHp == 0)
                {
                    barHealthItem.Break();
                }

                // damage counter
            }
        }
    }
    
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Counter"))
        {
            stopMovement = false;
        }
    }
}
