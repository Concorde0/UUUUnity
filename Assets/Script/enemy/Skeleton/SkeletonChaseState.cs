using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class SkeletonChaseState : GroundEnemyBaseState
{
    public float FixTime = 0.05f;
    public override void OnEnter(GroundEnemy enemy)
    {
        currentEnemy = enemy;
        currentEnemy.skeleton.isChase = true;
        currentEnemy.wait = false;
        currentEnemy.currentSpeed = currentEnemy.chaseSpeed;
        FixTime = 0.05f;
        

    }
    public override void LogicUpdate()
    {
        
        
        if (currentEnemy.character.currentHealth == 0 && currentEnemy.skeleton.canReborn)
        {
            currentEnemy.SwichState(NPCState.Reborn);
        }
        if (currentEnemy.character.currentHealth == 0 && currentEnemy.skeleton.canReborn == false)
        {
            currentEnemy.SwichState(NPCState.Dead);
        }

        if (FixTime >= 0)
        {
            FixTime -= Time.deltaTime;
        }

        if (FixTime <= 0)
        {
            if (currentEnemy.rb.transform.position.x < currentEnemy.playerPos.transform.position.x)
            {
                currentEnemy.rb.transform.localScale = new(1, 1, 1);
            }
            if (currentEnemy.rb.transform.position.x > currentEnemy.playerPos.transform.transform.position.x)
            {
                currentEnemy.rb.transform.localScale = new(-1, 1, 1);
            }

            if (Vector2.Distance(currentEnemy.transform.position,currentEnemy.playerPos.position) < currentEnemy.attackDistance&& currentEnemy.playerDead == false)
            {
                currentEnemy.SwichState(NPCState.Attack);
            }
            else
            {
                currentEnemy.anim.SetBool("run", true);
            }
        }
        
        


        
    }

    public override void PhysicsUpdate()
    {
        
    }
    public override void OnExit()
    {
        currentEnemy.anim.SetBool("run", false);
        currentEnemy.skeleton.isChase = false;
    }
}
