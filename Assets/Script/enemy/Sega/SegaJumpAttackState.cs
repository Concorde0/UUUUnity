using UnityEngine;

namespace Script.enemy.Sega
{
    
    public class SegaJumpAttackState : GroundEnemyBaseState
    {
        public float stateTime;
        public float stateTimeCounter;
        public override void OnEnter(GroundEnemy enemy)
        {
            currentEnemy = enemy;
            currentEnemy.currentSpeed = currentEnemy.chaseSpeed;
            stateTimeCounter = stateTime;
            currentEnemy.rb.AddForce(currentEnemy.transform.up*currentEnemy.sega.jumpAttackForce, ForceMode2D.Impulse);
            currentEnemy.anim.SetBool("jumpAttack",true);
        }

        public override void LogicUpdate()
        {
            stateTimeCounter -= Time.deltaTime;
            if (currentEnemy.character.currentHealth <= 0)
            {
                currentEnemy.SwichState(NPCState.Dead);
            }
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
            currentEnemy.anim.SetBool("jumpAttack",false);
        }
    }
}