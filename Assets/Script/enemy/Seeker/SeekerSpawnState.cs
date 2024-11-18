using UnityEngine;

    public class SeekerSpawnState : GroundEnemyBaseState
    {
        private float animCounter = 1f;
        public override void OnEnter(GroundEnemy enemy)
        {
            currentEnemy = enemy;
            Debug.Log("Entered SeekerSpawnState");
        }

        public override void LogicUpdate()
        {
            if (currentEnemy.seeker.isFind)
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
