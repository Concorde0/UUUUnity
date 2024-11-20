using UnityEngine;

namespace Script.enemy.Witcher
{
    public class WitcherDeadState : GroundEnemyBaseState
    {
        public override void OnEnter(GroundEnemy enemy)
        {
            currentEnemy = enemy;
            currentEnemy.anim.SetTrigger("dead");
            currentEnemy.rb.velocity = Vector3.zero;
            currentEnemy.isDead = true;
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