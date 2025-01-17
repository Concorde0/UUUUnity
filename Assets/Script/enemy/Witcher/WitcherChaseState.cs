using Unity.VisualScripting;
using UnityEngine;

namespace Script.enemy.Witcher
{
    public class WitcherChaseState : GroundEnemyBaseState
    {
        public override void OnEnter(GroundEnemy enemy)
        {
            currentEnemy = enemy;
            currentEnemy.currentSpeed = currentEnemy.witcher.witcherChaseSpeed;
            currentEnemy.anim.SetBool("chase",true);
        }

        public override void LogicUpdate()
        {
            if (currentEnemy.character.currentHealth <= 0)
            {
                currentEnemy.SwichState(NPCState.Dead);
            }
            
            if (Vector2.Distance(currentEnemy.transform.position, currentEnemy.playerPos.position) <
                currentEnemy.witcher.chaseDistance)
            {
                currentEnemy.witcher.isChase = false;
                currentEnemy.anim.SetBool("chase",false);
            }
            else
            {
                currentEnemy.witcher.isChase = true;
                currentEnemy.anim.SetBool("chase",true);
            }
            //Turn
            if (currentEnemy.rb.transform.position.x < currentEnemy.playerPos.transform.position.x)
            {
                currentEnemy.rb.transform.localScale = new(1.5f, 1.5f, 1.5f);
            }
            if (currentEnemy.rb.transform.position.x > currentEnemy.playerPos.transform.transform.position.x)
            {
                currentEnemy.rb.transform.localScale = new(-1.5f, 1.5f, 1.5f);
            }
            
            //Skeleton Magic
            if (currentEnemy.witcher.magic3CoolDownCounter <= 0 && currentEnemy.playerDead == false)
            {
                currentEnemy.SwichState(NPCState.Magic3);
            }
            
            //Push Magic
            if (Vector2.Distance(currentEnemy.transform.position,currentEnemy.playerPos.position) < currentEnemy.witcher.witcherMagic2Distance && currentEnemy.witcher.magic2CoolDownCounter<=0 && currentEnemy.playerDead == false)
            {
                currentEnemy.SwichState(NPCState.Magic2);
                currentEnemy.witcher.isMagic2 = true;
            }
            
            //Attack Magic
            if (currentEnemy.witcher.isMagic2 == false)
            {
                if (Vector2.Distance(currentEnemy.transform.position,currentEnemy.playerPos.position) < currentEnemy.witcher.witcherMagic1Distance&& currentEnemy.witcher.magic1CoolDownCounter <= 0 && currentEnemy.playerDead == false)
                {
                    currentEnemy.SwichState(NPCState.Magic1);
                } 
            }
            
        }

        public override void PhysicsUpdate()
        {
            
        }

        public override void OnExit()
        {
            currentEnemy.anim.SetBool("chase",false);
            currentEnemy.witcher.isChase = false;

        }
    }
}