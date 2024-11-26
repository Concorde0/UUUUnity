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
        currentEnemy.bandits.isChase = true;
    }

    public override void LogicUpdate()
    {
        if (currentEnemy.character.currentHealth <= 0)
        {
            currentEnemy.SwichState(NPCState.Dead);
        }
        if (currentEnemy.rb.transform.position.x < currentEnemy.playerPos.transform.position.x)
        {
            currentEnemy.rb.transform.localScale = new(-1, 1, 1);
        }
        else
        {
            currentEnemy.rb.transform.localScale = new(1, 1, 1);
        }
        
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
        currentEnemy.bandits.isChase = false;

    }
}
