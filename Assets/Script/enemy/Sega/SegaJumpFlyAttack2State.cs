using UnityEngine;

namespace Script.enemy.Sega
{
    public class SegaJumpFlyAttack2State : GroundEnemyBaseState
    {
        public float stateTime;
        public float stateTimeCounter;
        public override void OnEnter(GroundEnemy enemy)
        {
            currentEnemy = enemy;
            stateTimeCounter = stateTime;
            currentEnemy.anim.SetBool("jumpFlyAttack",true);
        }

        public override void LogicUpdate()
        {
            //重力变小，ie后变大，退出时正常
            stateTimeCounter -= Time.deltaTime;
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
            currentEnemy.anim.SetBool("jumpFlyAttack",false);
        }
    }
}