using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SkeletonDeadState : GroundEnemyBaseState
{
    public float fixTime = 0.05f;
        public override void OnEnter(GroundEnemy enemy)
        {
            currentEnemy = enemy;
            currentEnemy.currentSpeed = 0;
            currentEnemy.rb.velocity = Vector3.zero;
            currentEnemy.anim.SetBool("attack", false);
            currentEnemy.anim.SetBool("dead", true);
        }

        public override void LogicUpdate()
        {
            fixTime -= Time.deltaTime;
            if (fixTime < 0)
            {
                currentEnemy.anim.SetBool("dead", false);
            }
            
        }

        public override void PhysicsUpdate()
        {
            
        }

        public override void OnExit()
        {
            
        }
}
