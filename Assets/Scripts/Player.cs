using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(PlayerAnimation))]
public class Player : MonoBehaviour
{
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float playerSpeed = 2;
    [SerializeField] private LayerMask floorLayer;
    [SerializeField] private SpriteRenderer playerSprite;
    [SerializeField] private SpriteRenderer swardAttackSprite;


    private Rigidbody2D rigidBody;
    private readonly string horizontalAxis = "Horizontal";
    private float distanceToGroundWhenGrounded;
    private float distanceToGroundTolerance = 1.05f;
    private bool isGrounded;
    private bool isAttacking;
    private PlayerAnimation playerAnimation;
    
    private float regAttackAnimLen;
   

    void Start()
    {
        this.rigidBody = GetComponent<Rigidbody2D>();
        this.distanceToGroundWhenGrounded = GetDistanceToGround() * distanceToGroundTolerance;
        this.isGrounded = true;
        this.isAttacking = false;
        this.playerAnimation = this.GetComponent<PlayerAnimation>();
        this.regAttackAnimLen = this.playerSprite.GetComponent<Animator>().runtimeAnimatorController.animationClips.FirstOrDefault(clip => clip.name == "Attack")?.length ?? 0;        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
        RegAttack();
    }

    private void Move()
    {
        float input = Input.GetAxisRaw(horizontalAxis);
        if(this.isGrounded & !this.isAttacking)
        {
            this.rigidBody.velocity = new Vector2(input * this.playerSpeed, this.rigidBody.velocity.y);
            this.playerAnimation.Move(this.rigidBody.velocity.x);
            SetPlayerRotation(this.rigidBody.velocity.x);
        }  
    }

    private void Jump()
    {
        //In the check below rigidBody.velocity.y is very small only if the player is not in the air
        if (Input.GetKeyDown(KeyCode.Space) && this.isGrounded)
        {
            this.isGrounded = false;
            this.rigidBody.AddForce(new Vector2(0f, this.jumpForce), ForceMode2D.Impulse);
            this.playerAnimation.SetJumpAnimation(isJumping: true);
            StartCoroutine(UpdateJumpStatus());
        }
    }

    private float GetDistanceToGround()
    {
        RaycastHit2D hit = Physics2D.Raycast(this.transform.position, Vector2.down, 10f, this.floorLayer);        
        return hit.distance;        
    }

    IEnumerator UpdateJumpStatus()
    {
        yield return new WaitForSeconds(0.06f);

        while (GetDistanceToGround() > this.distanceToGroundWhenGrounded)
        {
            yield return new WaitForSeconds(0.05f);
        }

        this.isGrounded = true;
        this.playerAnimation.SetJumpAnimation(isJumping : false);
    }

    private void SetPlayerRotation(float xSpeed)
    {
        if (xSpeed > 0)
        {
            
            this.playerSprite.flipX = false;
            this.swardAttackSprite.flipY = false;

            Vector3 swardAttackNewPosition = this.swardAttackSprite.transform.localPosition;
            swardAttackNewPosition.x = Mathf.Abs(swardAttackNewPosition.x);
            this.swardAttackSprite.transform.localPosition = swardAttackNewPosition;
        }
        if (xSpeed < 0)
        {            
            playerSprite.flipX = true;
            swardAttackSprite.flipY = true;

            Vector3 swardAttackNewPosition = this.swardAttackSprite.transform.localPosition;
            swardAttackNewPosition.x = - Mathf.Abs(swardAttackNewPosition.x);
            this.swardAttackSprite.transform.localPosition = swardAttackNewPosition;
        }

    }

    private void RegAttack()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0) && isGrounded)
        {
            playerAnimation.RegAttack();
            StartCoroutine(AttackMode());
        }
    }

    IEnumerator AttackMode()
    {
        isAttacking = true;
        this.rigidBody.velocity = new Vector2(0, this.rigidBody.velocity.y);
        yield return new WaitForSeconds(regAttackAnimLen);
        isAttacking = false;
    }
}
