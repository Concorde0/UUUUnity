using UnityEngine;

    public class SeekerDeadState : GroundEnemyBaseState
    {
        public override void OnEnter(GroundEnemy enemy)
        {
            currentEnemy = enemy;
            currentEnemy.currentSpeed = 0;
            currentEnemy.rb.velocity = Vector3.zero;
            currentEnemy.anim.SetBool("attack", false);
            currentEnemy.anim.SetTrigger("dead");
        }

        public override void LogicUpdate()
        {
            
        }

        public override void PhysicsUpdate()
        {
        }

        public override void OnExit()
        {
        }
    }
