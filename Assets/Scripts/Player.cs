using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(PlayerAnimation))]
public class Player : MonoBehaviour
{
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float playerSpeed = 2;
    [SerializeField] LayerMask floorLayer;

    private Rigidbody2D rigidBody;
    private readonly string horizontalAxis = "Horizontal";
    private float distanceToGroundWhenGrounded;
    private float distanceToGroundTolerance = 1.05f;
    private bool isGrounded;
    private PlayerAnimation playerAnimation;
    private GameObject spriteChild;

    void Start()
    {
        this.rigidBody = GetComponent<Rigidbody2D>();
        this.distanceToGroundWhenGrounded = GetDistanceToGround() * distanceToGroundTolerance;
        this.isGrounded = true;
        playerAnimation = this.GetComponent<PlayerAnimation>();
        spriteChild = this.GetComponentInChildren<Animator>().gameObject;
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
            playerAnimation.Move(this.rigidBody.velocity.x);
            SetPlayerRotation(this.rigidBody.velocity.x);
        }  
    }

    private void Jump()
    {
        //In the check below rigidBody.velocity.y is very small only if the player is not in the air
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            isGrounded = false;
            this.rigidBody.AddForce(new Vector2(0f, this.jumpForce), ForceMode2D.Impulse);
            playerAnimation.Jump();
            StartCoroutine(UpdateJumpStatus());
        }
    }

    private float GetDistanceToGround()
    {
        RaycastHit2D hit = Physics2D.Raycast(this.transform.position, Vector2.down, 10f, floorLayer);        
        return hit.distance;        
    }

    IEnumerator UpdateJumpStatus()
    {
        yield return new WaitForSeconds(0.06f);

        while (GetDistanceToGround() > distanceToGroundWhenGrounded)
        {
            yield return new WaitForSeconds(0.05f);
        }

        isGrounded = true;
        playerAnimation.Land();
    }

    private void SetPlayerRotation(float xSpeed)
    {
        float playerRotation = this.transform.eulerAngles.y;        
        if(xSpeed > 0) playerRotation = 0;
        if (xSpeed < 0) playerRotation = 180;

        spriteChild.transform.eulerAngles = new Vector3(this.transform.rotation.x, playerRotation, this.transform.rotation.z);
    }
}
