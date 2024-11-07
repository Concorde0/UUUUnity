using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;



public class EyePatrolState : FlyEnemyBaseState
{
    public override void OnEnter(FlyEnemy enemy)
    {
        currentEnemy = enemy;
        currentEnemy.currentSpeed = currentEnemy.normalSpeed;
    }

    public override void LogicUpdate()
    {
        
        //move
        if (Vector2.Distance(currentEnemy.transform.position, currentEnemy.movePos.position) < 0.1f)
        {
            currentEnemy.movePos.position = currentEnemy.GetRandomPos();
            currentEnemy.wait = true;
        }

        //turn
        if (currentEnemy.rb.transform.position.x < currentEnemy.movePos.transform.position.x)
        {   
            currentEnemy.rb.transform.localScale = new(1, 1, 1);
        }
        if (currentEnemy.rb.transform.position.x > currentEnemy.movePos.transform.position.x)
        {
            currentEnemy.rb.transform.localScale = new(-1, 1, 1);
        }
        //change
        if (currentEnemy.foundPlayer == true)
        {
            currentEnemy.SwichState(NPCState.Hit);
        }
        

    }

    public override void PhysicsUpdate()
    {
        
    }
    public override void OnExit()
    {

    }
}
