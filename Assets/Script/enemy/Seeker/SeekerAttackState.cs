using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekerAttackState : GroundEnemyBaseState
{
    public float attackTime = 2f;
    public float attackTimeCounter;
    public override void OnEnter(GroundEnemy enemy)
    {
        currentEnemy = enemy;
        currentEnemy.attackPlayer = true;
        currentEnemy.currentSpeed = 0;
        currentEnemy.rb.velocity = Vector3.zero;
        currentEnemy.anim.SetBool("attack",true);
        attackTimeCounter = attackTime;
    }

    public override void LogicUpdate()
    {
        attackTimeCounter -= Time.deltaTime;
        if (attackTimeCounter <= 0)
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
