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
            Debug.Log("Escape");
            if (currentEnemy.character.currentHealth <= 0)
            {
                currentEnemy.SwichState(NPCState.Dead);
            }
            
            currentEnemy.rb.transform.localScale = new(2, 2, 2);
            if (Vector2.Distance(currentEnemy.transform.position,
                    currentEnemy.sega.jumpPosition.transform.position) < 0.5f)
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