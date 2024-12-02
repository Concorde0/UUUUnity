using UnityEngine;

namespace Script.enemy.Sega
{
    public class SegaWallMoveState : GroundEnemyBaseState
    {
        private float wallUpTime = 2f;
        private float wallUpTimeCounter;

        private bool isCheack;
        public override void OnEnter(GroundEnemy enemy)
        {
            currentEnemy = enemy;
            currentEnemy.anim.SetBool("wallmove",true);
            currentEnemy.rb.gravityScale = 0;
            wallUpTimeCounter = wallUpTime;
            
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
            
            if (isCheack)
            {
                wallUpTimeCounter -= Time.deltaTime;
            }
            if (wallUpTimeCounter <= 0)
            {
                currentEnemy.rb.gravityScale = 1;
                currentEnemy.SwichState(NPCState.Wait);
            }
            
            
            if(!currentEnemy.sega.isMoveWallPosition1 && !currentEnemy.sega.isUp)
            {
                Debug.Log("To 1 wall");

                currentEnemy.sega.MoveToWallPosition1();
                if (Vector2.Distance(currentEnemy.transform.position,
                        currentEnemy.sega.wallPosition1.transform.position) <=  1f)
                {
                    currentEnemy.SwichState(NPCState.WallAttack);
                }
            }
            
            
            
            if (currentEnemy.sega.isMoveWallPosition1 && !currentEnemy.sega.isUp)
            {
                Debug.Log("To 2 wall");
                currentEnemy.sega.MoveToWallPosition2();
                if (Vector2.Distance(currentEnemy.transform.position,
                        currentEnemy.sega.wallPosition2.transform.position) <= 1f)
                {
                    currentEnemy.sega.isUp = true;
                    currentEnemy.SwichState(NPCState.WallAttack);
                    
                }
                
            }
            
            
            if (currentEnemy.sega.isUp)
            {
                Debug.Log("To up");
                currentEnemy.sega.MoveToWallPositionUp();
                if (Vector2.Distance(currentEnemy.transform.position,
                        currentEnemy.sega.wallPositionUp.transform.position) <= 1f)
                {
                    isCheack = true;
                    currentEnemy.anim.SetBool("wallmove",false);
                    currentEnemy.anim.SetBool("wallup",true);
                    
                }
            }
            

            
            
            
            
            
            
            
            
        }

        public override void OnExit()
        {
            currentEnemy.anim.SetBool("wallmove",false);
            currentEnemy.anim.SetBool("wallup",false);

        }
    }
}