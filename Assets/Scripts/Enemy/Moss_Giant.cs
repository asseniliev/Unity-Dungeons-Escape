using System.Collections;
using UnityEngine;

public class Moss_Giant : Enemy
{
    [SerializeField] SpriteRenderer enemySprite;
    private Vector3 moveTarget;
    private Moss_Giant_Animation enemyAnimation;
    private float idleAnimLen;
    private bool isAttacking = false;
    private bool mustMove = false;

    void Start()
    {
        this.moveTarget = this.pointB.position;
        this.enemyAnimation = this.GetComponent<Moss_Giant_Animation>();
        this.idleAnimLen = this.enemyAnimation.GetIdleAnimationLength();
        StartCoroutine(WalkCycle());
    }


    public override void Update()
    {
        if (mustMove) Move();
    }

    public override void Attack()
    {
        Debug.Log(this.transform.name + " is attacking");
    }

    private void SwapMoveTarget()
    {
        if (this.moveTarget == this.pointA.position)
            this.moveTarget = this.pointB.position;
        else
            this.moveTarget = this.pointA.position;        
    }

    private void Move()
    {
        this.transform.position = Vector3.MoveTowards(this.transform.position, this.moveTarget, this.speed * Time.deltaTime);
    }

    private bool TargetPositionReached()
    {
        return Vector3.Distance(this.transform.position, this.moveTarget) < 0.01f;
    }

    IEnumerator WalkCycle()
    {
        while(!isAttacking)
        {
            //Debug.Log("Step1: Play idle animation");            
            yield return new WaitForSeconds(idleAnimLen);
            yield return new WaitForSeconds(0.06f);

            //Debug.Log("Step2: Trigger move animation");
            this.enemyAnimation.playMove();
            yield return new WaitForSeconds(0.06f);
            mustMove = true;

            //Debug.Log("Step3: do nothing while moving");
            while (!TargetPositionReached()) yield return null;

            //Debug.Log("Step4: trigger idle animation and wait until it is played");
            this.enemyAnimation.playIdle();
            mustMove = false;
            yield return new WaitForSeconds(idleAnimLen);

            //Debug.Log("Step5: Flip sprite and re-trigger idle animation");
            this.enemySprite.flipX = !this.enemySprite.flipX;
            this.enemyAnimation.playMove();
            this.enemyAnimation.playIdle();
            yield return new WaitForSeconds(0.05f);

            //Debug.Log("Step6: Swap target positions");
            SwapMoveTarget();
            yield return new WaitForSeconds(0.05f);
        }
    }
}
