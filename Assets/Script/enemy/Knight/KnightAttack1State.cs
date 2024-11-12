using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class KnightAttack1State : GroundEnemyBaseState
{
    public float attackWait = 4;
    public float attackWaitTimeConter;
    public override void OnEnter(GroundEnemy enemy)
    {
        currentEnemy = enemy;
        currentEnemy.knight.isAttack = true;
        attackWaitTimeConter = attackWait;
        currentEnemy.currentSpeed = 0;
        currentEnemy.anim.SetBool("attack1", true);
    }
    public override void LogicUpdate()
    {
        attackWaitTimeConter -= Time.deltaTime;
        if(attackWaitTimeConter <= 0)
        {
            currentEnemy.SwichState(NPCState.Patrol);
        }
        //3��״̬�� ����patrol,   ����ն�� => ����patrol.   ����idle => ׼���̻� || ��ת���� => ����patrol
    }

    public override void PhysicsUpdate()
    {
        
    }
    public override void OnExit()
    {
        currentEnemy.attackPlayer = false;
        currentEnemy.anim.SetBool("attack1", false);
        currentEnemy.knight.isAttack = false;
    }

}
