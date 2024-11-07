using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pig : GroundEnemy
{
    protected override void Awake()
    {
        base.Awake();
        patrolState = new pigPatrolState();
        chaseState = new PigChaseState(); 
    }
}
