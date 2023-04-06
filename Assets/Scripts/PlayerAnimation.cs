using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void Move(float moveSpeed)
    {
        animator.SetFloat("move", Mathf.Abs(moveSpeed));
    }

    public void SetJumpAnimation(bool isJumping)
    {
        animator.SetBool("isJumping", isJumping);
    }

    
}
