using UnityEngine;

namespace Script.enemy.Sega
{
    public class SegaJumpFlyAttack2State : GroundEnemyBaseState
    {
        private float stateTime = 0.6f;
        private float stateTimeCounter;

        private float recoverGravityTime = 0.3f;
        private float recoverGravityTimeCounter;

        // private float attackPlayerDistance = 3f;

        private bool isJump;
        private bool isTowards;
        public override void OnEnter(GroundEnemy enemy)
        {
            currentEnemy = enemy;
            stateTimeCounter = stateTime;
            recoverGravityTimeCounter = recoverGravityTime;
            currentEnemy.anim.SetBool("jumpFlyAttack2",true);
            currentEnemy.sega.rb.gravityScale = 0;
            
        }

        public override void LogicUpdate()
        {
            
            
            if (currentEnemy.character.currentHealth <= 0)
            {
                currentEnemy.SwichState(NPCState.Dead);
            }
            recoverGravityTimeCounter -= Time.deltaTime;
            if (recoverGravityTimeCounter <= 0 )
            {
                currentEnemy.rb.gravityScale = 1;
            }
            
            if (currentEnemy.sega.physiscCheck.isGround || currentEnemy.sega.physiscCheck.touchLeftWall || currentEnemy.sega.physiscCheck.touchRightWall)
            {
                currentEnemy.anim.SetBool("jumpFlyAttack3",true);
                isJump = true;
            }
            else
            {
                currentEnemy.sega.JumpFlyAttackTowards();
            }

            
            if (isJump)
            {
                stateTimeCounter -= Time.deltaTime;
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
            currentEnemy.anim.SetBool("jumpFlyAttack2",false);
            currentEnemy.anim.SetBool("jumpFlyAttack3",false);
            currentEnemy.sega.jumpFlyAttackTimeCounter = currentEnemy.sega.jumpFlyAttackCooldown;
            currentEnemy.rb.gravityScale = 1;

            isJump = false;
        }
    }
}