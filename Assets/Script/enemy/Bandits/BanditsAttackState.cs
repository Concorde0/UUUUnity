using UnityEngine;

namespace Script.enemy.Bandits
{
    public class BanditsAttackState : GroundEnemyBaseState
    {
        private float attackTime = 1.4f;
        private float attackTimeCounter;
        public override void OnEnter(GroundEnemy enemy)
        {
            currentEnemy = enemy;
            currentEnemy.currentSpeed = 0;
            currentEnemy.anim.SetBool("attack",true);
            attackTimeCounter = attackTime;
        }

        public override void LogicUpdate()
        {
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
            currentEnemy.anim.SetBool("attack",false);

        }
    }
}