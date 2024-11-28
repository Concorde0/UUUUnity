using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightIdleState : GroundEnemyBaseState
{
    public int randomNumber;
    public float attackWait = 0.5f;
    public float attackWaitTimeConter;
    public override void OnEnter(GroundEnemy enemy)
    {
        
        randomNumber = GetRandomNumber(5, 7);
        
        attackWaitTimeConter = attackWait;
        currentEnemy = enemy;
        currentEnemy.currentSpeed = 0;
        currentEnemy.rb.velocity = Vector2.zero;
        currentEnemy.anim.SetBool("idle2", true);
    }
    public override void LogicUpdate()
    {
        attackWaitTimeConter -= Time.deltaTime;
        if (attackWaitTimeConter <= 0)
        {
            if (randomNumber == 5)
            {
                currentEnemy.SwichState(NPCState.Whack);
            }
            if (randomNumber == 6)
            {
                currentEnemy.SwichState(NPCState.Stab);
            }
         
        }
    }

    public override void PhysicsUpdate()
    {
        
    }
    public override void OnExit()
    {
        currentEnemy.anim.SetBool("idle2", false);
        currentEnemy.rb.velocity = currentEnemy.rb.velocity.normalized;    
    }
    public int GetRandomNumber(int min, int max)
    {
        return Random.Range(min, max);
    }
}
