using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightAttack2State : GroundEnemyBaseState
{
    public float attackWait = 4;
    public float attackWaitTimeConter;
    public override void OnEnter(GroundEnemy enemy)
    {
        currentEnemy = enemy;
        currentEnemy.knight.isAttack = true;
        attackWaitTimeConter = attackWait;
        
        currentEnemy.currentSpeed = 0;
        currentEnemy.anim.SetBool("attack2", true);
    }
    public override void LogicUpdate()
    {
        attackWaitTimeConter -= Time.deltaTime;
        if (attackWaitTimeConter <= 0)
        {
            currentEnemy.SwichState(NPCState.Patrol);
        }
        //3种状态， 返回patrol,   进入斩击 => 返回patrol.   进入idle => 准备刺击 || 旋转攻击 => 返回patrol
    }

    public override void PhysicsUpdate()
    {

    }
    public override void OnExit()
    {
        currentEnemy.knight.isAttack = false;
        currentEnemy.attackPlayer = false;
        currentEnemy.anim.SetBool("attack2", false);
    }
}
