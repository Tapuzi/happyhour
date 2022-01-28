using System;
using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D body;
    

    private float horizontal;
    private float vertical;
    private float moveLimiter = 0.6f;

    private float runSpeed = 6.5f;
    private float crouchingSpeed = 3.0f;
    
    private Camera cam;

    private bool crouching = false;
    private bool stunned = false;

    public void Start()
    {
        body = GetComponent<Rigidbody2D>();
        cam = Camera.main;
    }

    public void Update()
    {
        // Gives a value between -1 and 1
        horizontal = Input.GetAxisRaw("Horizontal"); // -1 is left
        vertical = Input.GetAxisRaw("Vertical"); // -1 is down

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            FlipCrouching();
        }
    }

    private void FlipCrouching()
    {
        crouching = (!crouching);
        Debug.Log("Set crouching to " + crouching);
    }

    public void FixedUpdate()
    {
        if (stunned)
            return;
            
        if (horizontal != 0 && vertical != 0) // Check for diagonal movement
        {
            // limit movement speed diagonally, so you move at 70% speed
            horizontal *= moveLimiter;
            vertical *= moveLimiter;
        } 
        
        if(!crouching)
            body.velocity = new Vector2(horizontal * runSpeed, vertical * runSpeed);
        else
        {
            body.velocity = new Vector2(horizontal * crouchingSpeed, vertical * crouchingSpeed);
        }
        
        Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
		
        // rotate Y (green axis) towards mouse
        //transform.rotation = Quaternion.LookRotation(Vector3.forward, mousePos - transform.position);

        // rotate Y (green axis) away from mouse
        //transform.rotation = Quaternion.LookRotation(Vector3.forward, transform.position-mousePos);

        // rotate X (red axis) towards mouse
        Vector3 perpendicular = Vector3.Cross(transform.position-mousePos,Vector3.forward);
        transform.rotation = Quaternion.LookRotation(Vector3.forward, perpendicular);

        // rotate X (red axis) away from mouse
        //Vector3 perpendicular = Vector3.Cross(mousePos-transform.position,Vector3.forward);
        //transform.rotation = Quaternion.LookRotation(Vector3.forward, perpendicular);
		
        // using lookat
        // transform.LookAt(target.position, new Vector3(0, 0, -1));
    }

    public Transform GetTransform()
    {
        return GetComponent<Transform>();
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("EnemyProjectile") && !crouching)
            StartCoroutine(Stun(other.gameObject.GetComponent<EnemyProjectile>().GetStunTime()));
    }

    private IEnumerator Stun(float time)
    {
        stunned = true;
        yield return new WaitForSeconds(time);
        stunned = false;
    }

    public bool IsCrouching()
    {
        return crouching;
    }
}
