using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EyeBiteState : FlyEnemyBaseState
{
    public float biteTimeConter;
    public float biteTime = 1f;
    public Attack attack;
    public override void OnEnter(FlyEnemy enemy)
    {
        currentEnemy = enemy;
        currentEnemy.currentSpeed = currentEnemy.chaseSpeed;
        biteTimeConter = biteTime;
        
        //currentEnemy.isBite = true;
    }
    public override void LogicUpdate()
    {
        //Debug.Log("in bite");
        //turn
        if (currentEnemy.rb.transform.position.x < currentEnemy.playerPos.transform.position.x)
        {
            currentEnemy.rb.transform.localScale = new(1, 1, 1);
        }
        if (currentEnemy.rb.transform.position.x > currentEnemy.playerPos.transform.transform.position.x)
        {
            currentEnemy.rb.transform.localScale = new(-1, 1, 1);
        }
        if (currentEnemy.attackPlayer == true )
        {
            currentEnemy.anim.SetBool("bite", true);
            biteTimeConter -= Time.deltaTime;
        }

        
       if (biteTimeConter <= 0 )
       {
            currentEnemy.anim.SetBool("bite", false);
            currentEnemy.attackPlayer = false;
            currentEnemy.SwichState(NPCState.Tailattcak);
       }


    }
    
    
    public override void PhysicsUpdate()
    {
        
    }
    public override void OnExit()
    {

        currentEnemy.anim.SetBool("bite", false);
    }

}
