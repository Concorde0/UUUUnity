using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightPatrolState : GroundEnemyBaseState
{
   public int randomNumber;
    public override void OnEnter(GroundEnemy enemy)
    {
        randomNumber = GetRandomNumber(1, 4);
        //randomNumber = 3;
        currentEnemy = enemy;
        currentEnemy.currentSpeed = currentEnemy.normalSpeed;
        
    }
    public override void LogicUpdate()
    {
       
        //turn
        if (currentEnemy.rb.transform.position.x < currentEnemy.playerPos.transform.position.x)
        {
            currentEnemy.rb.transform.localScale = new(2, 2, 2);
        }
        if (currentEnemy.rb.transform.position.x > currentEnemy.playerPos.transform.transform.position.x)
        {
            currentEnemy.rb.transform.localScale = new(-2, 2, 2);
        }
        if (currentEnemy.knight.attackPlayer && !currentEnemy.knight.wait)
        {

            if (randomNumber == 1)
            {
                
                currentEnemy.SwichState(NPCState.Attack1);
            }
            else if (randomNumber == 2)
            {
                
                currentEnemy.SwichState(NPCState.Attack2);
            }
            if (randomNumber == 3)
            {
                
                currentEnemy.SwichState(NPCState.Idel2);
            }
           
        }
        else 
        {
            //Debug.Log("walk");
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
    public int GetRandomNumber(int min, int max)
    {
        return Random.Range(min, max);
    }
    
}
   

