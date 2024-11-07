using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeChaseState : FlyEnemyBaseState
{
    public Attack attack;
    public override void OnEnter(FlyEnemy enemy)
    {
        //Debug.Log("chase!");
        currentEnemy = enemy;
        currentEnemy.currentSpeed = currentEnemy.chaseSpeed;
    }
    public override void LogicUpdate()
    {
        //Debug.Log("in chase");
        //turn
        if (currentEnemy.rb.transform.position.x < currentEnemy.playerPos.transform.position.x)
        {
            currentEnemy.rb.transform.localScale = new(1, 1, 1);
        }
        if (currentEnemy.rb.transform.position.x > currentEnemy.playerPos.transform.transform.position.x)
        {
            currentEnemy.rb.transform.localScale = new(-1, 1, 1);
        }
        if (currentEnemy.attackPlayer == true )
        {
            currentEnemy.SwichState(NPCState.Attack);
        }

    }

    public override void PhysicsUpdate()
    {
        
    }
    public override void OnExit()
    {
        
    }

}
