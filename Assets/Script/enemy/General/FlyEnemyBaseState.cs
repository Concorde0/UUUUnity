using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FlyEnemyBaseState
{
    protected FlyEnemy currentEnemy;
    public abstract void OnEnter(FlyEnemy enemy);
    public abstract void LogicUpdate();
    public abstract void PhysicsUpdate();
    public abstract void OnExit();
}
