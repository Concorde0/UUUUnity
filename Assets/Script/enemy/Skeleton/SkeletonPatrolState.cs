using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class SkeletonPatrolState : GroundEnemyBaseState
{
    public override void OnEnter(GroundEnemy enemy)
    {
        currentEnemy = enemy;
        currentEnemy.currentSpeed = currentEnemy.normalSpeed;
        
    }
    public override void LogicUpdate()
    {
        currentEnemy.transform.position = Vector2.MoveTowards(currentEnemy.skeleton.transform.position,currentEnemy.skeleton.LeftPositon.position, currentEnemy.skeleton.currentSpeed * Time.deltaTime);
    }
    public override void PhysicsUpdate()
    {
        
    }
    public override void OnExit()
    {

    }
}
