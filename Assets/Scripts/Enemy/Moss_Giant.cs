using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moss_Giant : Enemy
{
    private Transform moveTarget;    

    void Start()
    {
        this.moveTarget = this.pointB;
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
        if (this.moveTarget.name == this.pointA.name)
            this.moveTarget = this.pointB;
        else
            this.moveTarget = this.pointA;        
    }

    private void Move()
    {
        Debug.Log(this.moveTarget);
        float step = this.speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(this.transform.position, this.moveTarget.position, step);        
        if(TargetPositionReached())
        {
            SwapMoveTarget();
        }
    }

    private bool TargetPositionReached()
    {
        Debug.Log(Vector3.Distance(this.transform.position, this.moveTarget.position));
        return Vector3.Distance(this.transform.position, this.moveTarget.position) < 0.1f;
    }

}
