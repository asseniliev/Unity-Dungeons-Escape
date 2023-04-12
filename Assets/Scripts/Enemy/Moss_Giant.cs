using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moss_Giant : Enemy
{
    private Vector3 moveTarget;    

    void Start()
    {
        this.moveTarget = this.pointB.position;
    }


    public override void Update()
    {
        Move();
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
        float step = this.speed * Time.deltaTime;
        this.transform.position = Vector3.MoveTowards(this.transform.position, this.moveTarget, step);        
        if(TargetPositionReached())
        {
            SwapMoveTarget();
        }
    }

    private bool TargetPositionReached()
    {
        return Vector3.Distance(this.transform.position, this.moveTarget) < 0.01f;
    }

}
