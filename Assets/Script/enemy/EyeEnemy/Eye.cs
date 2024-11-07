using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eye : FlyEnemy
{
    
    protected override void Awake()
    {
        base.Awake();
        patrolState = new EyePatrolState();
        chaseState = new EyeChaseState();
        hitState = new EyeBiteState();
        tailattackState = new EyeTailattackState();
    }
}
