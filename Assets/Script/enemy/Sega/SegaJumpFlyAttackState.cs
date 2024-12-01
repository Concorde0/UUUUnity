using UnityEngine;

namespace Script.enemy.Sega
{
    public class SegaJumpFlyAttackState : GroundEnemyBaseState
    {
        public float stateTime = 2f;
        public float stateTimeCounter;
        public override void OnEnter(GroundEnemy enemy)
        {
            currentEnemy = enemy;
            currentEnemy.rb.velocity = new Vector2(0f, 0f);
            currentEnemy.rb.AddForce(currentEnemy.transform.up * currentEnemy.sega.jumpFlyAttackForce, ForceMode2D.Impulse);
            //给一个速度！
            currentEnemy.anim.SetBool("jumpFlyAttack1",true);
            stateTimeCounter = stateTime;
        }

        public override void LogicUpdate()
        {
            Debug.Log("JumpFlyAttack1");
            stateTimeCounter -= Time.deltaTime;
            if (stateTimeCounter <= 0)
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
            currentEnemy.sega.jumpFlyAttack = false;


        }
    }
}