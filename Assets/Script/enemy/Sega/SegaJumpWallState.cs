using UnityEngine;

namespace Script.enemy.Sega
{
    public class SegaJumpWallState : GroundEnemyBaseState
    {
        public override void OnEnter(GroundEnemy enemy)
        {
            currentEnemy = enemy;
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