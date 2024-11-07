using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class EyeTailattackState : FlyEnemyBaseState
{
    public float tailTimeConter;
    public float tailTime = 0.65f;
    public override void OnEnter(FlyEnemy enemy)
    {
        tailTimeConter = tailTime;
        currentEnemy = enemy;
        currentEnemy.currentSpeed = currentEnemy.chaseSpeed;
        currentEnemy.anim.SetBool("tailattack", true);
        currentEnemy.isTail = true;
    }
    public override void LogicUpdate()
    {
        //Debug.Log("in tailattack");
        //turn
        if (currentEnemy.rb.transform.position.x < currentEnemy.playerPos.transform.position.x)
        {
            currentEnemy.rb.transform.localScale = new(1, 1, 1);
        }
        if (currentEnemy.rb.transform.position.x > currentEnemy.playerPos.transform.transform.position.x)
        {
            currentEnemy.rb.transform.localScale = new(-1, 1, 1);
        }
        tailTimeConter -= Time.deltaTime;
        
        
        if (tailTimeConter <= 0)
        {
            if (currentEnemy.foundPlayer == true  )
            {
                currentEnemy.anim.SetBool("tailattack", false);
                currentEnemy.isTail = false;
                //if(currentEnemy.isHurt) 
                currentEnemy.SwichState(NPCState.Hit);
            }
        }
        
    }
    public override void PhysicsUpdate()
    {
    }

    public override void OnExit()
    {
        currentEnemy.anim.SetBool("tailattack", false);
    }
}
