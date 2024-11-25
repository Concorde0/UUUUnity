using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanditsChaseState : GroundEnemyBaseState
{

    public override void OnEnter(GroundEnemy enemy)
    {
        currentEnemy = enemy;
        currentEnemy.currentSpeed = currentEnemy.chaseSpeed;
        currentEnemy.anim.SetBool("walk",true);
    }

    public override void LogicUpdate()
    {
        if (Vector2.Distance(currentEnemy.playerPos.position, currentEnemy.transform.position) <
            currentEnemy.attackDistance)
        {
            currentEnemy.SwichState(NPCState.Attack);
        }
    }

    public override void PhysicsUpdate()
    {
        
    }

    public override void OnExit()
    {
        currentEnemy.anim.SetBool("walk",false);
    }
}
