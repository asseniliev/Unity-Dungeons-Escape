using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sprider : Enemy
{
    public override void Attack()
    {
        Debug.Log(this.transform.name + " is attacking");
    }
}
