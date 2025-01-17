using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class SkeletonPatrolState : GroundEnemyBaseState
{
    private bool isChange;
    public override void OnEnter(GroundEnemy enemy)
    {
        currentEnemy = enemy;
        currentEnemy.currentSpeed = currentEnemy.normalSpeed;
        currentEnemy.skeleton.movePos = currentEnemy.skeleton.LeftPositon;
    }
    public override void LogicUpdate()
    {
        //hurt chase
        if (currentEnemy.character.currentHealth < currentEnemy.character.maxHealth)
        {
            isChange = true;
            currentEnemy.SwichState(NPCState.Chase);
        }
        
        if (currentEnemy.character.currentHealth == 0 && currentEnemy.skeleton.canReborn)
        {
            currentEnemy.SwichState(NPCState.Reborn);
        }
        if (currentEnemy.character.currentHealth == 0 && currentEnemy.skeleton.canReborn == false)
        {
            currentEnemy.SwichState(NPCState.Dead);
        }
        
        if (!currentEnemy.wait)
        {
            currentEnemy.anim.SetBool("walk",true);
        }
        else
        {
            currentEnemy.anim.SetBool("walk", false);
        }
        //Debug.Log("LogicUpdate");
        if (currentEnemy.FoundPlayer() && isChange == false)
        {
            currentEnemy.SwichState(NPCState.Chase);
        }

        
        if (Vector2.Distance(currentEnemy.skeleton.transform.position, currentEnemy.skeleton.movePos.position) < 0.1f)   
        {
            currentEnemy.skeleton.transform.localScale = new Vector3(1, 1, 1);
            currentEnemy.wait = true;
            currentEnemy.skeleton.movePos = currentEnemy.skeleton.RightPositon;
        }
        
        if (Vector2.Distance(currentEnemy.skeleton.transform.position, currentEnemy.skeleton.movePos.position) < 0.1f)
        {
            currentEnemy.skeleton.transform.localScale = new Vector3(-1, 1, 1);
            
            currentEnemy.wait = true;
            currentEnemy.skeleton.movePos = currentEnemy.skeleton.LeftPositon;
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
