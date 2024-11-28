using UnityEngine;

namespace Script.enemy.Sega
{
    public class SegaDeadState : GroundEnemyBaseState
    {
        public override void OnEnter(GroundEnemy enemy)
        {
            currentEnemy = enemy;
            currentEnemy.sega.isDead = true;
            currentEnemy.sega.statBar.SetActive(false);
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