using UnityEngine;

namespace Script.enemy.Sega
{
    public class SegaMagic1State : GroundEnemyBaseState
    {
        private float stateTime = 1.5f;
        private float stateTimeCounter;
        public override void OnEnter(GroundEnemy enemy)
        {
            currentEnemy = enemy;
            currentEnemy.anim.SetBool("magic1",true);
            currentEnemy.rb.velocity = new Vector2(0, 0);
            stateTimeCounter = stateTime;
        }

        public override void LogicUpdate()
        {
            
            
            if (currentEnemy.character.currentHealth <= 0)
            {
                currentEnemy.SwichState(NPCState.Dead);
            }
            Debug.Log("magic1");
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
            currentEnemy.anim.SetBool("magic1",false);
            currentEnemy.sega.magic1 = false;
            currentEnemy.sega.magic1CooldownTimeCounter = currentEnemy.sega.magic1Cooldown;
        }
    }
}