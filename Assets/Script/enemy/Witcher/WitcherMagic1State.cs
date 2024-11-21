using UnityEngine;

namespace Script.enemy.Witcher
{
    public class WitcherMagic1State : GroundEnemyBaseState
    {
        private float magic1Time = 2f;
        private float magic1TimeCounter;
        public override void OnEnter(GroundEnemy enemy)
        {
            currentEnemy = enemy;
            currentEnemy.anim.SetBool("magic1",true);
            magic1TimeCounter = magic1Time;
        }

        public override void LogicUpdate()
        {
            if (currentEnemy.character.currentHealth <= 0)
            {
                currentEnemy.SwichState(NPCState.Dead);
            }
            
            magic1TimeCounter -= Time.deltaTime;
            if (magic1TimeCounter <= 0)
            {
                currentEnemy.SwichState(NPCState.Chase);
            }
        }

        public override void PhysicsUpdate()
        {
            
        }

        public override void OnExit()
        {
            currentEnemy.anim.SetBool("magic1",false);
            currentEnemy.witcher.magic1CoolDownCounter = currentEnemy.witcher.magic1CoolDown;
        }
        
        
    }
}