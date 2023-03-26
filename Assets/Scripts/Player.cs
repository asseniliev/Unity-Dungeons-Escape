using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    //get handle to rigidbody
    private Rigidbody2D rigidBody;
    private string horizontalAxis = "Horizontal";
    private float playerSpeed = 2;
    private float jumpForce = 5f;
    private float groundDistance = 0.04f;  //Min 0.04

    Vector3 rayCastOffset;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        rayCastOffset = new Vector3(0, (GetComponent<BoxCollider2D>().size.y + groundDistance ) / 2 , 0);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
    }

    private void Move()
    {
        float input = Input.GetAxisRaw(horizontalAxis);
        rigidBody.velocity = new Vector2(input * playerSpeed, rigidBody.velocity.y);
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded())
        {
            rigidBody.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        }
    }

    private bool isGrounded()
    {
        Vector2 origin = this.transform.position - rayCastOffset;
        RaycastHit2D hit = Physics2D.Raycast(origin, -Vector2.up, groundDistance);
        if(hit.collider != null && hit.collider.name == "Floor") 
            return true;

        return false;
        
    }
}
