using UnityEngine;

namespace Script.enemy.Death
{
    public class DeathHideState : GroundEnemyBaseState
    {
        private float hideTime = 1.05f;
        private float hideTimeCounter;
        public override void OnEnter(GroundEnemy enemy)
        {
            currentEnemy = enemy;
            currentEnemy.rb.velocity = Vector3.zero;
            currentEnemy.anim.SetBool("hide",true);
            hideTimeCounter = hideTime;
            
        }

        public override void LogicUpdate()
        {
            if (currentEnemy.character.currentHealth <= 0)
            {
                currentEnemy.SwichState(NPCState.Dead);
            }
            hideTimeCounter -= Time.deltaTime;
            if (hideTimeCounter <= 0)
            {
                currentEnemy.SwichState(NPCState.Attack);
            }
        }

        public override void PhysicsUpdate()
        {
            
        }

        public override void OnExit()
        {
            currentEnemy.anim.SetBool("hide",false);
            currentEnemy.death.hideCooldownTimeCounter = currentEnemy.death.hideCooldown;
        }
    }
}