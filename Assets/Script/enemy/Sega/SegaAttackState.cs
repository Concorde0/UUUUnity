using UnityEngine;

namespace Script.enemy.Sega
{
    public class SegaAttackState : GroundEnemyBaseState
    {
        private bool issEscape;
        private float attackTime = 2.8f;
        private float attackTimeCounter;
        public override void OnEnter(GroundEnemy enemy)
        {
            currentEnemy = enemy;
            currentEnemy.rb.velocity = Vector2.zero;
            currentEnemy.attackPlayer = true;
            attackTimeCounter = attackTime;
            currentEnemy.anim.SetBool("attack",true);
        }

        public override void LogicUpdate()
        {
            //Escape
            if (currentEnemy.character.currentHealth <= currentEnemy.character.maxHealth / 1.5f && !issEscape)
            {
                issEscape = true;
                currentEnemy.anim.SetBool("attack", false);
                currentEnemy.SwichState(NPCState.Escape);
            }
            
            if (currentEnemy.character.currentHealth <= 0)
            {
                currentEnemy.SwichState(NPCState.Dead);
            }
            
            Debug.Log("attack");
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