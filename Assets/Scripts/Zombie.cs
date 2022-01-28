using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    private PlayerMovement player;
    private Vector3 playerPosition = Vector3.zero;
    [SerializeField] private float speed = 4f;
    private Rigidbody2D rb;
    
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
            playerPosition = player.GetPosition();
        }
        else
        {
            playerPosition = Vector3.zero;
        }

        FollowPlayer();
    }

    private void FollowPlayer()
    {
        if (playerPosition == Vector3.zero)
        {
            return;
        }

        Vector3 pos = Vector3.MoveTowards(transform.position, playerPosition, speed * Time.fixedDeltaTime);
        rb.MovePosition(pos);
        transform.LookAt(playerPosition);
    }
}
