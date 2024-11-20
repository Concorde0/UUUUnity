using UnityEngine;

namespace Script.enemy.Death
{
    public class DeathChaseState : GroundEnemyBaseState
    {
        public override void OnEnter(GroundEnemy enemy)
        {
            currentEnemy = enemy;
            currentEnemy.currentSpeed = currentEnemy.chaseSpeed;
            currentEnemy.death.isChase = true;
            currentEnemy.anim.SetBool("chase",true);
        }

        public override void LogicUpdate()
        {
            if (currentEnemy.character.currentHealth <= 0)
            {
                currentEnemy.SwichState(NPCState.Dead);
            }
            //Turn
            if (currentEnemy.rb.transform.position.x < currentEnemy.playerPos.transform.position.x)
            {
                currentEnemy.rb.transform.localScale = new(-1, 1, 1);
            }
            if (currentEnemy.rb.transform.position.x > currentEnemy.playerPos.transform.transform.position.x)
            {
                currentEnemy.rb.transform.localScale = new(1, 1, 1);
            }
            
            //Hide
            if (Vector2.Distance(currentEnemy.transform.position,currentEnemy.playerPos.position) > currentEnemy.death.hideDistance && currentEnemy.death.hideCooldownTimeCounter<=0)
            {
                currentEnemy.SwichState(NPCState.Hide);
            }

            if (currentEnemy.death.hideCooldownTimeCounter > 0)
            {
                //Magic
                if (Vector2.Distance(currentEnemy.transform.position,currentEnemy.playerPos.position) > currentEnemy.death.magicDistance && currentEnemy.death.magicCooldownTimeCounter<=0)
                {
                    currentEnemy.SwichState(NPCState.Magic);
                }
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
            currentEnemy.anim.SetBool("chase",false);
            currentEnemy.death.isChase = false;
        }
    }
}