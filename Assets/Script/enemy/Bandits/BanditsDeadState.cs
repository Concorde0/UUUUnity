using UnityEngine;

namespace Script.enemy.Bandits
{
    public class BanditsDeadState : GroundEnemyBaseState
    {
        public override void OnEnter(GroundEnemy enemy)
        {
            currentEnemy = enemy;
            currentEnemy.rb.velocity = Vector3.zero;
            currentEnemy.currentSpeed = 0;
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
}