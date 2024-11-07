//using System.Collections;
//using System.Collections.Generic;
//using Unity.VisualScripting;
//using UnityEngine;
//using static UnityEngine.RuleTile.TilingRuleOutput;

//public class BeeAttackState : FlyEnemyBaseState
//{
//    private BeeAttackController beeAttackController;
    
//    public override void OnEnter(FlyEnemy enemy)
//    {
//        currentEnemy = enemy;
//        currentEnemy.currentSpeed = currentEnemy.normalSpeed;
        

//    }
//    public override void LogicUpdate()
//    {
//        if (currentEnemy.flyFoundPlayer == true)
//        {
//            //currentEnemy.anim.SetBool("attack", true);
//            beeAttackController.ChasePlayer();
//        }

//    }
  
//    public override void PhysicsUpdate()
//    {

//    }

//    public override void OnExit()
//    {
//        //currentEnemy.anim.SetBool("attack", false); 
//        currentEnemy.lostTimeCounter = currentEnemy.lostTime;
        
//    }
    

//}
