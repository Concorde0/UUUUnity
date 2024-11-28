using UnityEngine;

namespace Script.enemy.Sega
{
    public class SegaChaseState : GroundEnemyBaseState
    {
        public override void OnEnter(GroundEnemy enemy)
        {
            currentEnemy = enemy;
            currentEnemy.currentSpeed = currentEnemy.normalSpeed;
            currentEnemy.sega.isChase = true;
            currentEnemy.anim.SetBool("chase",true);
        }

        public override void LogicUpdate()
        {
            //Turn
            if (currentEnemy.rb.transform.position.x < currentEnemy.playerPos.transform.position.x)
            {
                currentEnemy.rb.transform.localScale = new(-2, 2, 2);
            }
            if (currentEnemy.rb.transform.position.x > currentEnemy.playerPos.transform.transform.position.x)
            {
                currentEnemy.rb.transform.localScale = new(2, 2, 2);
            }
            
            //JumpAttack
            if (Vector2.Distance(currentEnemy.transform.position,currentEnemy.playerPos.position) < currentEnemy.sega.jumpAttackDistance)
            {
                currentEnemy.SwichState(NPCState.JumpAttack);
            }
            
            //Magic1
            if (currentEnemy.sega.magic1CooldownTimeCounter <= 0)
            {
                currentEnemy.SwichState(NPCState.Magic1);
            }
            
            //Magic2
            if (currentEnemy.sega.magic2CooldownTimeCounter <= 0)
            {
                currentEnemy.SwichState(NPCState.Magic2);
            }
            
            //JumpFlyState
            if (currentEnemy.sega.jumpFlyAttackTimeCounter <= 0)
            {
                currentEnemy.SwichState(NPCState.JumpFlyAttack);
            }
            
            //Escape
            if (currentEnemy.character.currentHealth <= currentEnemy.character.maxHealth / 2)
            {
                currentEnemy.SwichState(NPCState.Escape);
            }
            
            
            //Attack
            if (Vector2.Distance(currentEnemy.transform.position,currentEnemy.playerPos.position) < currentEnemy.attackDistance)
            {
                currentEnemy.SwichState(NPCState.Attack);
            }
            
            
        }

        public override void PhysicsUpdate()
        {
            
        }

        public override void OnExit()
        {
            currentEnemy.sega.isChase = false;

            currentEnemy.anim.SetBool("chase",false);
        }
    }
}