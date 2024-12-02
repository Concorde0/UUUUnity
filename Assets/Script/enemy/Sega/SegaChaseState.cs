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
            Debug.Log("chase");
            
            //dead
            if (currentEnemy.character.currentHealth <= 0)
            {
                currentEnemy.SwichState(NPCState.Dead);
            }
            
            //Turn
            if (currentEnemy.rb.transform.position.x < currentEnemy.playerPos.transform.position.x)
            {
                currentEnemy.rb.transform.localScale = new(2, 2, 2);
            }
            if (currentEnemy.rb.transform.position.x > currentEnemy.playerPos.transform.transform.position.x)
            {
                currentEnemy.rb.transform.localScale = new(-2, 2, 2);
            }
            
            //Escape
            if (currentEnemy.character.currentHealth <= currentEnemy.character.maxHealth / 2)
            {
                currentEnemy.SwichState(NPCState.Escape);
            }
            
            //JumpFlyAttack
            if (currentEnemy.sega.jumpFlyAttackTimeCounter <= 0)
            {
                currentEnemy.SwichState(NPCState.JumpFlyAttack);
                currentEnemy.sega.jumpFlyAttack = true;
            }
            
            //JumpAttack
            if (Vector2.Distance(currentEnemy.transform.position,currentEnemy.playerPos.position) > currentEnemy.sega.jumpAttackDistance && currentEnemy.sega.jumpFlyAttack == false)
            {
                currentEnemy.SwichState(NPCState.JumpAttack);
            }
            
            //Magic2
            if (currentEnemy.sega.magic2CooldownTimeCounter <= 0)
            {
                currentEnemy.SwichState(NPCState.Magic2);
                currentEnemy.sega.magic2 = true;
            }
            
            //Magic1
            if (currentEnemy.sega.magic1CooldownTimeCounter <= 0)
            {
                currentEnemy.SwichState(NPCState.Magic1);
                currentEnemy.sega.magic1 = true;
            }
            
            
            
            
            //Attack2
            if (currentEnemy.sega.attack2CooldownTimeCounter <= 0 && Vector2.Distance(currentEnemy.transform.position,currentEnemy.playerPos.position) < currentEnemy.attackDistance)
            {
                currentEnemy.SwichState(NPCState.Attack2);
                currentEnemy.sega.attack2 = true;
            }
            
            
            
            
            //Attack
            if (Vector2.Distance(currentEnemy.transform.position,currentEnemy.playerPos.position) < currentEnemy.attackDistance
                && currentEnemy.sega.attack2 == false 
                && currentEnemy.sega.jumpFlyAttack ==false 
                && currentEnemy.sega.magic2 == false 
                && currentEnemy.sega.magic1 == false)
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