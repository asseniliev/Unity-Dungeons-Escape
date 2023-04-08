using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private Animator swardAnimator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Move(float moveSpeed)
    {
        playerAnimator.SetFloat("move", Mathf.Abs(moveSpeed));
    }

    public void SetJumpAnimation(bool isJumping)
    {
        playerAnimator.SetBool("isJumping", isJumping);
    }

    public void RegAttack()
    {
        playerAnimator.SetTrigger("attack");
        swardAnimator.SetTrigger("attack");
    }

    
}
