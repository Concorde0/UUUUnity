using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pigPatrolState : GroundEnemyBaseState
{
    
    public override void OnEnter(GroundEnemy enemy)
    {
        
        currentEnemy = enemy;
        currentEnemy.currentSpeed = currentEnemy.normalSpeed;
    }
    public override void LogicUpdate()
    {
        //Debug.Log("pig in patrol");
        if(currentEnemy.FoundPlayer())
        {
            currentEnemy.SwichState(NPCState.Chase);
        }

        if (!currentEnemy.physiscCheck.isGround ||(currentEnemy.physiscCheck.touchLeftWall && currentEnemy.faceDir.x < 0) || (currentEnemy.physiscCheck.touchRightWall && currentEnemy.faceDir.x > 0))
        {
            currentEnemy.wait = true;
            currentEnemy.anim.SetBool("walk", false);
        }
        else
        {
            currentEnemy.anim.SetBool("walk", true);
        }
        
    }

    public override void PhysicsUpdate()
    {
        
    }
    public override void OnExit()
    {
        currentEnemy.anim.SetBool("walk", false);
    }
}
