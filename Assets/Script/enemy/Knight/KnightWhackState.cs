using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightWhackState : GroundEnemyBaseState
{
    public float attackWait = 5;
    public float attackWaitTimeConter;
    public override void OnEnter(GroundEnemy enemy)
    {
        currentEnemy = enemy;
        currentEnemy.knight.isAttack = true;
        attackWaitTimeConter = attackWait;
        currentEnemy.rb.velocity = Vector2.zero;

        currentEnemy.currentSpeed = 0;
        currentEnemy.anim.SetBool("whack", true);
    }
    public override void LogicUpdate()
    {
        attackWaitTimeConter -= Time.deltaTime;
        if (attackWaitTimeConter <= 0)
        {
            currentEnemy.SwichState(NPCState.Patrol);
        }
    }

    public override void PhysicsUpdate()
    {

    }
    public override void OnExit()
    {
        currentEnemy.knight.isAttack = false;
        currentEnemy.attackPlayer = false;
        currentEnemy.anim.SetBool("whack",false);
        currentEnemy.rb.velocity = currentEnemy.rb.velocity.normalized;
    }
}
