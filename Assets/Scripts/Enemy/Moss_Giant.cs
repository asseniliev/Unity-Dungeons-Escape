using System.Collections;
using UnityEngine;

public class Moss_Giant : Enemy
{
    [SerializeField] SpriteRenderer mossGiantSprite;
    private Vector3 moveTarget;
    private Moss_Giant_Animation mossGiantAnim;
    private bool targetPositionAnimRunning = false;
    private float idleAnimLen;
    private bool isAttacking = false;
    private bool mustMove = false;

    void Start()
    {
        this.moveTarget = this.pointB.position;
        this.mossGiantAnim = this.GetComponent<Moss_Giant_Animation>();
        this.idleAnimLen = this.mossGiantAnim.GetIdleAnimationLength();
        StartCoroutine(WalkCycle());
    }


    public override void Update()
    {
        if (mustMove) Move();

        //if (Input.GetKeyDown(KeyCode.Mouse1)) isAttacking = true;

    }

    // Start is called before the first frame update

    public override void Attack()
    {
        //throw new System.NotImplementedException();
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
            //Debug.Log("Step1");
            //this.mossGiantAnim.playIdle();
            yield return new WaitForSeconds(idleAnimLen);
            yield return new WaitForSeconds(0.06f);

            //Debug.Log("Step2");
            this.mossGiantAnim.playMove();
            yield return new WaitForSeconds(0.06f);
            mustMove = true;

            //Debug.Log("Step3");
            while (!TargetPositionReached()) yield return null;

            //Debug.Log("Step4");
            this.mossGiantAnim.playIdle();
            mustMove = false;
            yield return new WaitForSeconds(idleAnimLen);

            this.mossGiantSprite.flipX = !this.mossGiantSprite.flipX;
            this.mossGiantAnim.playMove();
            this.mossGiantAnim.playIdle();
            yield return new WaitForSeconds(0.05f);

            SwapMoveTarget();
            yield return new WaitForSeconds(0.05f);

            //this.mossGiantAnim.playIdle();
        }
    }
}
