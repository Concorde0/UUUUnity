using UnityEngine;

namespace Script.enemy.Sega
{
    public class SegaWaitState : GroundEnemyBaseState
    {

        private float waitFix = 5f;
        private float waitFixTimeCounter;
        public override void OnEnter(GroundEnemy enemy)
        {
            currentEnemy = enemy;
            currentEnemy.anim.SetBool("chase",true);
            waitFixTimeCounter = waitFix;
        }

        public override void LogicUpdate()
        {
            
        }

        public override void PhysicsUpdate()
        {
            
            if (currentEnemy.character.currentHealth <= 0)
            {
                currentEnemy.SwichState(NPCState.Dead);
            }
            Debug.Log("wait");
            waitFixTimeCounter -= Time.deltaTime;
            currentEnemy.sega.WaitPosition();

            if (waitFixTimeCounter <= 0)
            {
                if (Vector2.Distance(currentEnemy.transform.position, currentEnemy.playerPos.transform.position) <=
                    5f)
                {
                    currentEnemy.SwichState(NPCState.Chase);
                } 
            }
            
        }

        public override void OnExit()
        {
            currentEnemy.anim.SetBool("chase",false);

        }
    }
}