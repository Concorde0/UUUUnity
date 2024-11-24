using UnityEngine;

namespace Script.enemy.Witcher
{
    public class WitcherWaitState : GroundEnemyBaseState
    {
        public override void OnEnter(GroundEnemy enemy)
        {
            currentEnemy = enemy;
            currentEnemy.currentSpeed = 0;
            
        }

        public override void LogicUpdate()
        {
            if (currentEnemy.FoundPlayer())
            {
                currentEnemy.foundPlayer = true;
                currentEnemy.SwichState(NPCState.Chase);
                currentEnemy.witcher.statBar.SetActive(true);
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