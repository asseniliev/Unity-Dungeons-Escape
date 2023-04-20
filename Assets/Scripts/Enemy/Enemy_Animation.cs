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

    public void playIdle()
    {
        this.enemyAnimator.SetTrigger("Idle");
    }

    public void playMove()
    {
        this.enemyAnimator.SetTrigger("Move");
    }
}
