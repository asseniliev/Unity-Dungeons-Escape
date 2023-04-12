using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Moss_Giant_Animation : MonoBehaviour
{
    [SerializeField] private Animator enemyAnimator;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
