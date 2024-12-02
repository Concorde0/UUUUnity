using UnityEngine;

namespace Script.enemy.Sega
{
    public class SegaJumpWallState : GroundEnemyBaseState
    {
        public override void OnEnter(GroundEnemy enemy)
        {
            currentEnemy = enemy;
            currentEnemy.anim.SetBool("jumpwall",true);
            currentEnemy.rb.gravityScale = 0;
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
            Debug.Log("jumpWall");
            currentEnemy.sega.JumpToWallPosition();
            if (Vector2.Distance(currentEnemy.transform.position,
                    currentEnemy.sega.jumpWallPosition.transform.position) <= 1f)
            {
                currentEnemy.SwichState(NPCState.WallMove);
            }
        }

        public override void OnExit()
        {
            currentEnemy.anim.SetBool("jumpwall",false);

        }
    }
}