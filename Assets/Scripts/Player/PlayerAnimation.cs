using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private Animator swardAnimator;

    public void Move(float moveSpeed)
    {
        this.playerAnimator.SetFloat("move", Mathf.Abs(moveSpeed));
    }

    public void SetJumpAnimation(bool isJumping)
    {
        this.playerAnimator.SetBool("isJumping", isJumping);
    }

    public void RegAttack()
    {
        this.playerAnimator.SetTrigger("attack");
        this.swardAnimator.SetTrigger("attack");
    }

    public float GetRegAttackAnimationLength()
    {
        return this.playerAnimator.runtimeAnimatorController.animationClips.FirstOrDefault(clip => clip.name == "Attack")?.length ?? 0;
    }

    
}
