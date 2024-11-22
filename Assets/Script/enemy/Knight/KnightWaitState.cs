using UnityEngine;

namespace Script.enemy.Knight
{
    public class KnightWaitState : GroundEnemyBaseState
    {
        public override void OnEnter(GroundEnemy enemy)
        {
            currentEnemy = enemy;
            currentEnemy.wait = true;
            currentEnemy.currentSpeed = 0;
        }

        public override void LogicUpdate()
        {
            if (currentEnemy.FoundPlayer())
            {
                currentEnemy.SwichState(NPCState.Patrol);
            }
        }

        public override void PhysicsUpdate()
        {
        }

        public override void OnExit()
        {
            currentEnemy.wait = false;
        }
    }
}