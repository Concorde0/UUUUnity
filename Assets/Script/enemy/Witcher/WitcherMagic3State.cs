using UnityEngine;

namespace Script.enemy.Witcher
{
    public class WitcherMagic3State : GroundEnemyBaseState
    {
        private float magic3Time = 2.4f;
        private float magic3TimeCounter;
        public override void OnEnter(GroundEnemy enemy)
        {
            currentEnemy = enemy;
            magic3TimeCounter = magic3Time;
            currentEnemy.anim.SetBool("magic3", true);
        }

        public override void LogicUpdate()
        {
            magic3TimeCounter -= Time.deltaTime;
            if (magic3TimeCounter <= 0)
            {
                currentEnemy.SwichState(NPCState.Chase);
            }
        }

        public override void PhysicsUpdate()
        {
        }

        public override void OnExit()
        {
            currentEnemy.anim.SetBool("magic3", false);
            currentEnemy.witcher.magic3CoolDownCounter = currentEnemy.witcher.magic3CoolDown;

        }
    }
}