using UnityEngine;
using UnityEngine.EventSystems;

namespace Script.enemy.Sega
{
    public class SegaJumpAttack2State : GroundEnemyBaseState
    {
        private float stateTime = 0.4f;
        private float stateTimeCounter;
        public override void OnEnter(GroundEnemy enemy)
        {
            currentEnemy = enemy;
            currentEnemy.anim.SetBool("jumpAttack2",true);
            currentEnemy.sega.isChase = true;
            stateTimeCounter = stateTime;
            currentEnemy.currentSpeed = currentEnemy.sega.jumpAttackSpeed;
        }

        public override void LogicUpdate()
        {
            Debug.Log("jumpAttack2");
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
            currentEnemy.anim.SetBool("jumpAttack2",false);
            currentEnemy.sega.isChase = false;

        }
    }
}