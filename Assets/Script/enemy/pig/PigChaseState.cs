using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigChaseState : GroundEnemyBaseState
{
    public override void OnEnter(GroundEnemy enemy)
    {
        currentEnemy = enemy;
        currentEnemy.currentSpeed = currentEnemy.chaseSpeed;
        currentEnemy.anim.SetBool("run",true);
    }
    public override void LogicUpdate()
    {
        //Debug.Log("pig in chase");
        if (currentEnemy.lostTimeCounter <= 0)
        {
            currentEnemy.SwichState(NPCState.Patrol);
        } 
        if (!currentEnemy.physiscCheck.isGround || (currentEnemy.physiscCheck.touchLeftWall && currentEnemy.faceDir.x < 0) || (currentEnemy.physiscCheck.touchRightWall && currentEnemy.faceDir.x > 0))
        {
            currentEnemy.transform.localScale = new Vector3(currentEnemy.faceDir.x  , 1, 1);
        }
    }

    public override void PhysicsUpdate()
    {
        
    }
    public override void OnExit()
    {
        currentEnemy. lostTimeCounter = currentEnemy.lostTime;
        currentEnemy.anim.SetBool("run",false);
    }
}

