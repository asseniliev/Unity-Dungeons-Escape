using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] SpriteRenderer enemySprite;
    [SerializeField] protected int health;
    [SerializeField] protected float speed;
    [SerializeField] protected int gems;
    [SerializeField] protected Transform pointA, pointB;

    protected Vector3 moveTarget;
    protected bool isAttacking = false;
    protected bool mustMove = false;

    private Enemy_Animation enemyAnimation;
    private float idleAnimLen;

    public virtual void Start()
    {
        this.moveTarget = this.pointB.position;
        this.enemyAnimation = this.GetComponent<Enemy_Animation>();
        this.idleAnimLen = this.enemyAnimation.GetIdleAnimationLength();
        StartCoroutine(WalkCycle());
    }

    public virtual void Update()
    {
        if (mustMove) Move();
    }


    private void SwapMoveTarget()
    {
        if (this.moveTarget == this.pointA.position)
            this.moveTarget = this.pointB.position;
        else
            this.moveTarget = this.pointA.position;
    }

    public virtual void Move()
    {
        this.transform.position = Vector3.MoveTowards(this.transform.position, this.moveTarget, this.speed * Time.deltaTime);
    }

    private bool TargetPositionReached()
    {
        return Vector3.Distance(this.transform.position, this.moveTarget) < 0.01f;
    }

    public abstract void Attack();

    IEnumerator WalkCycle()
    {
        while (!isAttacking)
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
