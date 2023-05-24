using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WalkCycleIdle : StateMachineBehaviour
{
    bool isInCombat;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
        EventManager.eventManager.CallAnimStartEvent("Idle", animator.transform.root.name);
        Debug.Log("IdleAnimIteration = " + animator.GetInteger("IdleAnimIteration"));
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        isInCombat = animator.GetBool("isInCombat");
        if (stateInfo.normalizedTime > 1 && !isInCombat) 
        {
            
            if (animator.GetInteger("IdleAnimIteration") == 2)
            {
                //Debug.Log("Update trigger 'MOVE' at frame " + Time.frameCount);                
                EventManager.eventManager.CallAnimEndEvent("Idle", animator.transform.root.name);
                animator.SetTrigger("Walk");
                animator.SetInteger("IdleAnimIteration",  1);
            }
            else
            {
                //Debug.Log("Update trigger 'IDLE' at frame " + Time.frameCount);                
                EventManager.eventManager.CallFlipSpriteEvent();
                animator.SetTrigger("Idle");
                animator.SetInteger("IdleAnimIteration", 2);
            }
        }
        
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Debug.Log("OnStateExit ");
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Implement code that processes and affects root motion
        //Debug.Log("OnStateMove ");
    }

    // OnStateIK is called right after Animator.OnAnimatorIK()
    override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Implement code that sets up animation IK (inverse kinematics)
        //Debug.Log("OnStateIK ");
    }
}
