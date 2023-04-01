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
    private float distanceToGroundWhenGrounded;
    private float distanceToGroundTolerance = 1.05f;
    private bool isGrounded;

    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float playerSpeed = 2;
    [SerializeField] LayerMask floorLayer;

    void Start()
    {
        this.rigidBody = GetComponent<Rigidbody2D>();
        this.distanceToGroundWhenGrounded = getDistanceToGround() * distanceToGroundTolerance;
        this.isGrounded = true;
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
        if(isGrounded)
        {
            this.rigidBody.velocity = new Vector2(input * playerSpeed, this.rigidBody.velocity.y);
        }  
    }

    private void Jump()
    {
        //In the check below rigidBody.velocity.y is very small only if the player is not in the air
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            isGrounded = false;
            this.rigidBody.AddForce(new Vector2(0f, this.jumpForce), ForceMode2D.Impulse);            
            StartCoroutine(UpdateJumpStatus());
        }
    }

    private float getDistanceToGround()
    {
        RaycastHit2D hit = Physics2D.Raycast(this.transform.position, Vector2.down, 10f, floorLayer);        
        return hit.distance;        
    }

    IEnumerator UpdateJumpStatus()
    {
        yield return new WaitForSeconds(0.06f);

        while (getDistanceToGround() > distanceToGroundWhenGrounded)
        {
            yield return new WaitForSeconds(0.05f);
        }

        isGrounded = true;
    }

    
}
