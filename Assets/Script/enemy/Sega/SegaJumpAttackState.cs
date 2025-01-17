using UnityEngine;

namespace Script.enemy.Sega
{
    
    public class SegaJumpAttackState : GroundEnemyBaseState
    {
        // private float fixJumpAttackTime = 3f;
        // private float fixJumpAttackTimeCounter;
        //
        private float jumpAttackDistance = 4f;
        public override void OnEnter(GroundEnemy enemy)
        {
            currentEnemy = enemy;
            currentEnemy.currentSpeed = currentEnemy.chaseSpeed;
            currentEnemy.sega.isChase = true;

            // fixJumpAttackTimeCounter = fixJumpAttackTime;
            currentEnemy.rb.AddForce(currentEnemy.transform.up*currentEnemy.sega.jumpAttackForce, ForceMode2D.Impulse);
            currentEnemy.anim.SetBool("jumpAttack1",true);
        }

        public override void LogicUpdate()
        {

            if (currentEnemy.character.currentHealth <= 0)
            {
                currentEnemy.SwichState(NPCState.Dead);
            }
            
            if (currentEnemy.character.currentHealth <= 0)
            {
                currentEnemy.SwichState(NPCState.Dead);
            }
            if (Vector2.Distance(currentEnemy.transform.position,currentEnemy.playerPos.position) < jumpAttackDistance)
            {
                currentEnemy.SwichState(NPCState.JumpAttack2);
            }

            if (currentEnemy.physiscCheck.touchLeftWall || currentEnemy.physiscCheck.touchRightWall)
            {
                currentEnemy.SwichState(NPCState.JumpAttack2);
            }
        }

        public override void PhysicsUpdate()
        {
            
        }

        public override void OnExit()
        {
            currentEnemy.anim.SetBool("jumpAttack1",false);
            currentEnemy.sega.isChase = false;
            
        }
    }
}