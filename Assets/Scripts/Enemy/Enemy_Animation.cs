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

    public void playIdle()
    {
        this.enemyAnimator.SetTrigger("Idle");
        this.enemyAnimator.SetInteger("PrevState", 1);
    }

    public void playMove()
    {
        this.enemyAnimator.SetTrigger("Move");
        this.enemyAnimator.SetInteger("PrevState", 2);
    }

    public void playHit()
    {
        this.enemyAnimator.SetTrigger("BeenHit");
    }
}
