using UnityEngine;

namespace Script.enemy.Death
{
    public class DeathAttackState : GroundEnemyBaseState
    {
        private float attackTime = 1.7f;
        private float attackTimeCounter;
        public override void OnEnter(GroundEnemy enemy)
        {
            
            currentEnemy = enemy;
            currentEnemy.currentSpeed = 0;
            currentEnemy.rb.velocity = Vector3.zero;
            currentEnemy.attackPlayer = true;
            attackTimeCounter = attackTime;
            currentEnemy.anim.SetBool("attack",true);
            
        }

        public override void LogicUpdate()
        {
            if (currentEnemy.character.currentHealth <= 0)
            {
                currentEnemy.SwichState(NPCState.Dead);
            }
            attackTimeCounter -= Time.deltaTime;
            if (attackTimeCounter <= 0)
            {
                currentEnemy.SwichState(NPCState.Chase);
            }
        }

        public override void PhysicsUpdate()
        {
            
        }

        public override void OnExit()
        {
            currentEnemy.attackPlayer = false;
            currentEnemy.anim.SetBool("attack", false);
        }
    }
}