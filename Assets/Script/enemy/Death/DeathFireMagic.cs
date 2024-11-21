using UnityEngine;

namespace Script.enemy.Death
{
    public class DeathFireMagic : GroundEnemyBaseState
    {
        private float fireTime = 0.8f;
        private float fireTimeCounter;
        public override void OnEnter(GroundEnemy enemy)
        {
            Debug.Log("fireOnEnter");

            currentEnemy = enemy;
            currentEnemy.rb.velocity = Vector3.zero;
            currentEnemy.anim.SetBool("fire",true);
            fireTimeCounter = fireTime;
        }

        public override void LogicUpdate()
        {
            Debug.Log("fire");
            if (currentEnemy.character.currentHealth <= 0)
            {
                currentEnemy.SwichState(NPCState.Dead);
            }
            fireTimeCounter -= Time.deltaTime;
            if (fireTimeCounter <= 0)
            {
                currentEnemy.SwichState(NPCState.Chase);
            }
        }

        public override void PhysicsUpdate()
        {
            
        }

        public override void OnExit()
        {
            Debug.Log("fireOnExit");

            currentEnemy.anim.SetBool("fire",false);
            currentEnemy.death.fireCooldownTimeCounter = currentEnemy.death.fireCooldown;
        }
    }
}