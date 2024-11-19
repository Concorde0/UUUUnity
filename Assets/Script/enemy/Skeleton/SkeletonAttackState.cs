using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonAttackState : GroundEnemyBaseState
{
    private float attackTime = 3.8f;
    private float attackTimeCounter;
    public override void OnEnter(GroundEnemy enemy)
    {
        //currentEnemy.rb.velocity = Vector2.zero;
        currentEnemy = enemy;
        currentEnemy.attackPlayer = true;
        currentEnemy.currentSpeed = 0;
        currentEnemy.rb.velocity = Vector3.zero;
        currentEnemy.anim.SetBool("attack",true);
        currentEnemy.anim.SetBool("walk",false);
        attackTimeCounter = attackTime;
    }
    public override void LogicUpdate()
    {
        attackTimeCounter -= Time.deltaTime;
        
        if (currentEnemy.character.currentHealth == 0 && currentEnemy.skeleton.canReborn)
        {
            currentEnemy.SwichState(NPCState.Reborn);
        }
        if (currentEnemy.character.currentHealth == 0 && currentEnemy.skeleton.canReborn == false)
        {
            currentEnemy.SwichState(NPCState.Dead);
        }

        if (attackTimeCounter <= 0)
        {
            currentEnemy.SwichState(NPCState.Chase);
        }
        
    }

    public override void PhysicsUpdate()
    {
        
    }
    public override void OnExit()
    {
        currentEnemy.attackPlayer = false;
        currentEnemy.anim.SetBool("attack", false);
    }
}
