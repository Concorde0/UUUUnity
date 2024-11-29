using UnityEngine;

namespace Script.enemy.Sega
{
    public class SegaAttack2State : GroundEnemyBaseState
    {
        private float stateTime = 2.5f;
        private float stateTimeCounter;
        public override void OnEnter(GroundEnemy enemy)
        {
            currentEnemy = enemy;
            currentEnemy.anim.SetBool("attack2",true);
            stateTimeCounter = stateTime;
        }

        public override void LogicUpdate()
        {
            Debug.Log("Attack2");
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
            
            currentEnemy.anim.SetBool("attack2",false);
            currentEnemy.sega.attack2CooldownTimeCounter = currentEnemy.sega.attack2Cooldown;
        }
    }
}