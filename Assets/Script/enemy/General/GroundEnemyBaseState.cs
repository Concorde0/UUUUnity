using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GroundEnemyBaseState
{
    protected GroundEnemy currentEnemy;
    public abstract void OnEnter(GroundEnemy enemy);
    public abstract void LogicUpdate();
    public abstract void PhysicsUpdate();
    public abstract void OnExit();
}
