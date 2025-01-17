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
            if (!Mathf.Approximately(currentEnemy.character.currentHealth, currentEnemy.character.maxHealth))
            {
                currentEnemy.SwichState(NPCState.Chase);
            }
            
            if (currentEnemy.character.currentHealth <= 0)
            {
                currentEnemy.SwichState(NPCState.Dead);
            }
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