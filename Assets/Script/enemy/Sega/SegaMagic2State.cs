using UnityEngine;

namespace Script.enemy.Sega
{
    public class SegaMagic2State : GroundEnemyBaseState
    {
        private float stateTime = 1.5f;
        private float stateTimeCounter;
        public override void OnEnter(GroundEnemy enemy)
        {
            currentEnemy = enemy;
            currentEnemy.rb.velocity = Vector2.zero;
            currentEnemy.anim.SetBool("magic2",true);
            stateTimeCounter = stateTime;
        }

        public override void LogicUpdate()
        {
            currentEnemy.sega.Resilience();
            
            if (currentEnemy.character.currentHealth <= 0)
            {
                currentEnemy.SwichState(NPCState.Dead);
            }
            stateTimeCounter -= Time.deltaTime;
            if (stateTimeCounter <= 0)
            {
                currentEnemy.SwichState(NPCState.Chase);
            }
            
        }

        public override void PhysicsUpdate()
        {
            
        }

        public override void OnExit()
        {
            currentEnemy.anim.SetBool("magic2",false);
            currentEnemy.sega.magic2 = false;

            currentEnemy.sega.magic2CooldownTimeCounter = currentEnemy.sega.magic2Cooldown;
        }
    }
}