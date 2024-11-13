using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class SkeletonRebornState : GroundEnemyBaseState
{
    public float timeCounter = 3.5f;
    public float fixTime = 0.05f;
    public override void OnEnter(GroundEnemy enemy)
    {
        currentEnemy = enemy;
        currentEnemy.currentSpeed = 0;
        currentEnemy.rb.velocity = Vector3.zero;
        currentEnemy.anim.SetBool("attack",false);
        currentEnemy.anim.SetTrigger("Treborn");
        

    }
    public override void LogicUpdate()
    {
        currentEnemy.character.currentHealth = 10;
        timeCounter -= Time.deltaTime;
        fixTime -= Time.deltaTime;
        if (timeCounter <= 0)
        {
            currentEnemy.SwichState(NPCState.Chase);   
        }
    }

    public override void PhysicsUpdate()
    {
        
    }
    public override void OnExit()
    {
        currentEnemy.skeleton.canReborn = false;
        
    }


}
