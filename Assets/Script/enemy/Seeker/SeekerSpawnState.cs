using UnityEngine;

    public class SeekerSpawnState : GroundEnemyBaseState
    {
        private float animCounter = 1f;
        private bool isChange;
        public override void OnEnter(GroundEnemy enemy)
        {
            currentEnemy = enemy;
        }

        public override void LogicUpdate()
        {
            if (currentEnemy.character.currentHealth < currentEnemy.character.maxHealth)
            {
                isChange = true;
                currentEnemy.anim.SetBool("spawn",true);
                animCounter -= Time.deltaTime;
                if (animCounter <= 0)
                {
                    currentEnemy.SwichState(NPCState.Chase);
                }
            }
            
            if (currentEnemy.character.currentHealth <= 0)
            {
                currentEnemy.SwichState(NPCState.Dead);
            }
            
            if (currentEnemy.seeker.isFind && isChange == false)
            {
                currentEnemy.anim.SetBool("spawn",true);
                animCounter -= Time.deltaTime;
                if (animCounter <= 0)
                {
                    currentEnemy.SwichState(NPCState.Chase);
                }
                
            }
        }

        public override void PhysicsUpdate()
        {
            
        }

        public override void OnExit()
        {
            currentEnemy.anim.SetBool("spawn",false);
        }
    }
