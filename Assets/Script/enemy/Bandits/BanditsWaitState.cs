using UnityEngine;

namespace Script.enemy.Bandits
{
    public class BanditsWaitState : GroundEnemyBaseState
    {
        public override void OnEnter(GroundEnemy enemy)
        {
            currentEnemy = enemy;
            
        }

        public override void LogicUpdate()
        {
            if (currentEnemy.FoundPlayer())
            {
                currentEnemy.SwichState(NPCState.Chase);
            }
        }

        public override void PhysicsUpdate()
        {
        }

        public override void OnExit()
        {
        }
    }
}