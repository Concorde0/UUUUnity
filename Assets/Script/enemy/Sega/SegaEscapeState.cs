using UnityEngine;

namespace Script.enemy.Sega
{
    public class SegaEscapeState : GroundEnemyBaseState
    {
        public override void OnEnter(GroundEnemy enemy)
        {
            currentEnemy = enemy;
            currentEnemy.sega.isEscape = true;
            currentEnemy.anim.SetBool("chase",true);
        }

        public override void LogicUpdate()
        {
            if (Vector2.Distance(currentEnemy.transform.position,
                    currentEnemy.sega.jumpWallPosition.transform.position) < 0.1f)
            {
                currentEnemy.SwichState(NPCState.JumpWall);
            }
            
        }

        public override void PhysicsUpdate()
        {
            
        }

        public override void OnExit()
        {
            currentEnemy.anim.SetBool("chase",false);
            currentEnemy.sega.isEscape = false;
        }
    }
}