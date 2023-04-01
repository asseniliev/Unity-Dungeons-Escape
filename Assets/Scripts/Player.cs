using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    //get handle to rigidbody
    private Rigidbody2D rigidBody;
    private readonly string horizontalAxis = "Horizontal";    

    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float playerSpeed = 2;
    [SerializeField] LayerMask floorLayer;

    void Start()
    {
        this.rigidBody = GetComponent<Rigidbody2D>();
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
        if(Mathf.Abs(this.rigidBody.velocity.y) < 10E-5)
        {
            this.rigidBody.velocity = new Vector2(input * playerSpeed, this.rigidBody.velocity.y);
        }  
    }

    private void Jump()
    {
        //In the check below rigidBody.velocity.y is very small only if the player is not in the air
        if (Input.GetKeyDown(KeyCode.Space) && Mathf.Abs(this.rigidBody.velocity.y) < 10E-5)        
        {
            this.rigidBody.AddForce(new Vector2(0f, this.jumpForce), ForceMode2D.Impulse);
        }
    }
}
