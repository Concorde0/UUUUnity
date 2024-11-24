using UnityEngine;

namespace Script.enemy.Death
{
    public class DeathWaitState : GroundEnemyBaseState
    {
       
        public override void OnEnter(GroundEnemy enemy)
        {
            currentEnemy = enemy;
            currentEnemy.currentSpeed = 0;
        }

        public override void LogicUpdate()
        {
            if (currentEnemy.character.currentHealth <= 0)
            {
                currentEnemy.SwichState(NPCState.Dead);
            }
            //Chase
            if (currentEnemy.FoundPlayer())
            {
                currentEnemy.death.statBar.SetActive(true);
                currentEnemy.foundPlayer = true;
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