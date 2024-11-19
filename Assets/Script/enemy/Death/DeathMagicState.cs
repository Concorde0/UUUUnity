using UnityEngine;

namespace Script.enemy.Death
{
    public class DeathMagicState : GroundEnemyBaseState
    {
        private float magicTime = 0.8f;
        private float magicTimeCounter;
        public override void OnEnter(GroundEnemy enemy)
        {
            Debug.Log("Death Magic State");
            currentEnemy = enemy;
            currentEnemy.rb.velocity = Vector3.zero;
            currentEnemy.anim.SetBool("magic",true);
            magicTimeCounter = magicTime;
        }

        public override void LogicUpdate()
        {
            if (currentEnemy.character.currentHealth <= 0)
            {
                currentEnemy.SwichState(NPCState.Dead);
            }
            magicTimeCounter -= Time.deltaTime;
            if (magicTimeCounter <= 0)
            {
                currentEnemy.SwichState(NPCState.Chase);
            }
        }

        public override void PhysicsUpdate()
        {
            
        }

        public override void OnExit()
        {
            currentEnemy.anim.SetBool("magic",false);
            currentEnemy.death.magicCooldownTimeCounter = currentEnemy.death.magicCooldown;
        }
    }
}