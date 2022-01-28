using System;
using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody body;
    [SerializeField] GameObject mapPlane;
    [SerializeField] GameObject rayStart;

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
        body = GetComponent<Rigidbody>();
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
            body.velocity = new Vector3(horizontal * runSpeed, 0, vertical * runSpeed);
        else
        {
            body.velocity = new Vector3(horizontal * crouchingSpeed, 0, vertical * crouchingSpeed);
        }
        
        //Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        
        
        
        Ray mousePos = cam.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(mousePos.origin, mousePos.direction * 100, Color.red);
        RaycastHit hitinfo;
        
        if(mapPlane.GetComponent<Collider>().Raycast(mousePos, out hitinfo, 100000f))
        {
            Debug.Log(hitinfo);
            transform.rotation = Quaternion.LookRotation(transform.position - hitinfo.point, Vector3.up);
        }
        else
        {
            Debug.Log("not working");
        }
        
        /*
        Vector3 mouse = Input.mousePosition;
        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(new Vector3(
            mouse.x, 
            mouse.y,
            transform.position.y));
        Vector3 forward = mouseWorld - transform.position;
        transform.rotation = Quaternion.LookRotation(forward, Vector3.up);
        */
        // rotate Y (green axis) towards mouse
        //transform.rotation = Quaternion.LookRotation(Vector3.up, hitinfo.point);

        // rotate Y (green axis) away from mouse
        //transform.rotation = Quaternion.LookRotation(Vector3.forward, transform.position-mousePos);

        // rotate X (red axis) towards mouse
        //Vector3 perpendicular = Vector3.Cross(transform.position-mousePos,Vector3.forward);
        //transform.rotation = Quaternion.LookRotation(Vector3.forward, perpendicular);


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
