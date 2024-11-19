using UnityEngine;

namespace Script.enemy.Death
{
    public class DeathDeadState : GroundEnemyBaseState
    {
        public override void OnEnter(GroundEnemy enemy)
        {
            currentEnemy = enemy;
            currentEnemy.death.isDead = true;
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