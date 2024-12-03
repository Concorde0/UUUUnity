using UnityEngine;

namespace Script.enemy.Sega
{
    public class SegaWallAttackState : GroundEnemyBaseState
    {
        private float attackTime = 1.4f;
        private float attackTimeCounter;

        private bool isCheck;
        private bool isEscape;
        
        public override void OnEnter(GroundEnemy enemy)
        {
            currentEnemy = enemy;
            currentEnemy.anim.SetBool("wallwait", true);   
            currentEnemy.rb.velocity = new Vector2(0f, 0f);
            attackTimeCounter = attackTime;
            
        }

        public override void LogicUpdate()
        {
            if (currentEnemy.character.currentHealth <= 0)
            {
                currentEnemy.SwichState(NPCState.Dead);
            }
            Debug.Log("wallWait");
            
            if (isCheck)
            {
                attackTimeCounter -= Time.deltaTime;
            }
            
            if (attackTimeCounter <= 0)
            {
                currentEnemy.SwichState(NPCState.WallMove);
            }
            
            if (Vector2.Distance(currentEnemy.transform.position, currentEnemy.playerPos.transform.position) < 3f )
            {
                Debug.Log("wallAttack");
                currentEnemy.anim.SetBool("wallattack",true);
                currentEnemy.anim.SetBool("wallwait", false);  
                isCheck = true;
            }
        }

        public override void PhysicsUpdate()
        {
            
        }

        public override void OnExit()
        {
            if (currentEnemy.sega.isMoveWallPosition1)
            {
                currentEnemy.sega.isUp = true;
            }
            currentEnemy.sega.isMoveWallPosition1 = true;

            currentEnemy.anim.SetBool("wallwait", false);
            isCheck = false;

        }
    }
}