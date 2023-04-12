using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public bool idleAnimationNotPlaying()
    {
        return !this.enemyAnimator.GetCurrentAnimatorStateInfo(0).IsName("Idle");
    }

    public void playIdle()
    {
        this.enemyAnimator.SetTrigger("Idle");
    }
}
