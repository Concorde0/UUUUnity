using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : GroundEnemy
{
    public GroundEnemyBaseState attackState;
    public GroundEnemyBaseState reBornState;
    public Transform LeftPositon;
    public Transform RightPositon;
    protected override void Awake()
    {
        patrolState = new SkeletonPatrolState();
        chaseState = new SkeletonChaseState();
        attackState = new SkeletonAttackState();
        reBornState = new SkeletonRebornState();
    }
 

    public override void SwichState(NPCState state)
    {
        var newState = state switch
        {
            NPCState.Patrol => patrolState,
            NPCState.Chase => chaseState,
            NPCState.Attack => attackState,
            NPCState.Rebone => reBornState,

            _ => null
        };
        currentState.OnExit();
        currentState = newState;
        currentState.OnEnter(this);
    }
}




