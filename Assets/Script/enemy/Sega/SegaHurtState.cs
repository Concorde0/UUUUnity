using UnityEngine;

namespace Script.enemy.Sega
{
    
    public class SegaHurtState : GroundEnemyBaseState
    {
        private float hurtTime = 0.05f;
        private float hurtTimeCounter;

        private float StateTime = 2.5f;
        private float StateTimeCounter;

        private float isHurt;
        
        public override void OnEnter(GroundEnemy enemy)
        {
            currentEnemy = enemy;
            currentEnemy.rb.velocity = Vector3.zero;
            hurtTimeCounter = hurtTime;
            StateTimeCounter = StateTime;
            currentEnemy.anim.SetTrigger("Hurt");
        }

        public override void LogicUpdate()
        {
            
            if (currentEnemy.character.currentHealth <= 0)
            {
                currentEnemy.SwichState(NPCState.Dead);
            }
            
            StateTimeCounter -= Time.deltaTime;
            if (StateTimeCounter <= 0)
            {
                currentEnemy.SwichState(NPCState.Chase);
            }
        }

        public override void PhysicsUpdate()
        {
            hurtTimeCounter -= Time.deltaTime;
            if (hurtTimeCounter >= 0)
            {
                currentEnemy.rb.AddForce(new Vector2(currentEnemy.sega.hurtForceHorizontal,currentEnemy.sega.hurtForceVertical), ForceMode2D.Impulse);
            }
            
        }

        public override void OnExit()
        {
            
        }
    }
}