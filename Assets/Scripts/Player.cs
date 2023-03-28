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
    float distanceToFloorWhenGrounder;

    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float playerSpeed = 2;
    [SerializeField] LayerMask floorLayer;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        distanceToFloorWhenGrounder = MeasureDistanceToFloor();
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
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())        {
            
            rigidBody.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        }
    }

    private bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(this.transform.position, Vector2.down, distanceToFloorWhenGrounder * 10, floorLayer);
        
        if (hit.collider == null) 
            return false;

        float currentDistanceToFloor = Mathf.Abs(this.transform.position.y - hit.collider.transform.position.y);

        if (currentDistanceToFloor > distanceToFloorWhenGrounder) 
            return false;
        
        return true;
        
    }

    private float MeasureDistanceToFloor()
    {
        RaycastHit2D hit = Physics2D.Raycast(this.transform.position, Vector2.down, 10.0f, floorLayer);
        return (this.transform.position.y - hit.collider.transform.position.y);
    }
}
