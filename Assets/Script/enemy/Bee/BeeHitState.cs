using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class BeeHitState : FlyEnemyBaseState
{
    public bool arrow;
    public float continueTime = 2;
    
    public override void OnEnter(FlyEnemy enemy)
    {
        currentEnemy = enemy;
        currentEnemy.currentSpeed = 0;
        currentEnemy.anim.SetBool("hit", true);
        arrow = true;
        
    }
    public override void LogicUpdate()
    {   
        continueTime -= Time.deltaTime;
        if (currentEnemy.foundPlayer == false)
        {
            currentEnemy.SwichState(NPCState.Patrol);
        }
        if(continueTime <= 0 && currentEnemy.foundPlayer == true)
        {
            arrow = false;
            currentEnemy.SwichState(NPCState.Attack);
        }
        
       
    }

    
    public override void PhysicsUpdate()
    {
        
    }
    public override void OnExit()
    {
        
        currentEnemy.anim.SetBool("hit", false);
    }
}
