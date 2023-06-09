using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Enemy_Animation : MonoBehaviour
{
    [SerializeField] protected Animator enemyAnimator;

    public float GetIdleAnimationLength()
    {
        return this.enemyAnimator.runtimeAnimatorController.animationClips.FirstOrDefault(clip => clip.name == "Idle")?.length ?? 0;
    }

    public float GetHitAnimationLength()
    {
        return this.enemyAnimator.runtimeAnimatorController.animationClips.FirstOrDefault(clip => clip.name == "Hit")?.length ?? 0;
    }

    public void GetCurrentStateInfo()
    {
        AnimatorClipInfo[] clipInfo = this.enemyAnimator.GetCurrentAnimatorClipInfo(0);
        Debug.Log(clipInfo[0].clip.name);

        //this.enemyAnimator.runtimeAnimatorController.animationClips.FirstOrDefault(x => x.GetHashCode() == stateHash);
    }

    public void PlayIdle()
    {
        this.enemyAnimator.SetTrigger("Idle");
    }

    public void PlayMove()
    {
        this.enemyAnimator.SetTrigger("Move");
    }

    public void PlayHit()
    {
        this.enemyAnimator.SetTrigger("BeenHit");
    }

    public void SetInCombatMode(bool value)
    {
        this.enemyAnimator.SetBool("IsInCombat", value);
    }
}
