using UnityEngine;

namespace Script.enemy.Witcher
{
    public class WitcherMagic2State : GroundEnemyBaseState
    {
        private float magic2Time = 2f;
        private float magic2TimeCounter;
        public override void OnEnter(GroundEnemy enemy)
        {
            currentEnemy = enemy;
            currentEnemy.anim.SetBool("magic2",true);
            magic2TimeCounter = magic2Time;
        }

        public override void LogicUpdate()
        {
            magic2TimeCounter -= Time.deltaTime;
            if (magic2TimeCounter <= 0)
            {
                currentEnemy.SwichState(NPCState.Chase);
            }
        }

        public override void PhysicsUpdate()
        {
            
        }

        public override void OnExit()
        {
            currentEnemy.anim.SetBool("magic2",false);
            currentEnemy.witcher.magic2CoolDownCounter = currentEnemy.witcher.magic2CoolDown;
            currentEnemy.witcher.isMagic2 = false;

        }
    }
}