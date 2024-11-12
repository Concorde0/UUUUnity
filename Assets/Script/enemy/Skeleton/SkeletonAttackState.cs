using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonAttackState : GroundEnemyBaseState
{
    public float attackWait = 4;
    public float attackWaitTimeConter;
    public override void OnEnter(GroundEnemy enemy)
    {
        currentEnemy = enemy;
        currentEnemy.currentSpeed = 0;
        currentEnemy.attackPlayer = true;//×÷ÓÃÎª  ÈÃmove() ==0
        attackWaitTimeConter =  attackWait;
    }
    public override void LogicUpdate()
    {
        currentEnemy.anim.SetBool("attack",true);
        attackWaitTimeConter -= Time.deltaTime;
        if(attackWaitTimeConter < 0)
        {
            currentEnemy.SwichState(NPCState.Chase);
        }
    }

    public override void PhysicsUpdate()
    {
        
    }
    public override void OnExit()
    {
        currentEnemy.attackPlayer = false;
        currentEnemy.anim.SetBool("attack", false);
    }
}
