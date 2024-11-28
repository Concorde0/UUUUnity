using UnityEngine;

namespace Script.enemy.Sega
{
    public class SegaJumpFlyAttackState : GroundEnemyBaseState
    {
        public override void OnEnter(GroundEnemy enemy)
        {
            currentEnemy = enemy;
            currentEnemy.rb.AddForce(currentEnemy.transform.up * currentEnemy.sega.jumpFlyAttackForce, ForceMode2D.Impulse);
            currentEnemy.anim.SetBool("jumpFlyAttack1",true);
        }

        public override void LogicUpdate()
        {
            if (currentEnemy.rb.velocity.y <= 0)
            {
                currentEnemy.SwichState(NPCState.JumpFlyAttack2);
            }
        }

        public override void PhysicsUpdate()
        {
            
        }

        public override void OnExit()
        {
            currentEnemy.anim.SetBool("jumpFlyAttack1",false);

        }
    }
}