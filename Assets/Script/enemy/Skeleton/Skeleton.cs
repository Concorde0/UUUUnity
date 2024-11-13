using System;
using DG.Tweening.Plugins.Options;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class Skeleton : GroundEnemy
{
    private GroundEnemyBaseState attackState;
    private GroundEnemyBaseState reBornState;
    private GroundEnemyBaseState deadState;
    public Transform LeftPositon;
    public Transform RightPositon;
    public Transform movePos;
    public bool isChase;
    public bool canReborn = true;



    protected override void Awake()
    {
        base.Awake();
        movePos = LeftPositon;
        patrolState = new SkeletonPatrolState();
        chaseState = new SkeletonChaseState();
        attackState = new SkeletonAttackState();
        reBornState = new SkeletonRebornState();
        deadState = new SkeletonDeadState();
    }

    protected override void FixedUpdate()
    {

        if (!isHurt && !isDead && !wait && !attackPlayer && !isChase )
        {
            Move();
        }

        if (!isHurt && !isDead && !wait && !attackPlayer && isChase)
        {
            
            Chase();
        }

        currentState.PhysicsUpdate();
    }

    public override void SwichState(NPCState state)
    {
        var newState = state switch
        {
            NPCState.Patrol => patrolState,
            NPCState.Chase => chaseState,
            NPCState.Attack => attackState,
            NPCState.Reborn => reBornState,
            NPCState.Dead => deadState,
            

            _ => null
        };
        currentState.OnExit();
        currentState = newState;
        currentState.OnEnter(this);
    }
    

    public override void OnDie()
    {
        gameObject.layer = 2;
        anim.SetBool("dead", true);
        isDead = false;
    }

    public override void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position, movePos.position, currentSpeed * Time.deltaTime);
    }

    private void Chase()
    {
        rb.velocity = new Vector2(currentSpeed * -faceDir.x * Time.deltaTime, rb.velocity.y);
    }




    // ReSharper disable Unity.PerformanceAnalysis
    public override void TimeCounter()
    {

        if (wait)
        {
            Debug.Log("Wait");
            waitTimeCounter -= Time.deltaTime;
            if (waitTimeCounter <= 0)
            {
                wait = false;
                waitTimeCounter = waitTime;
            }

        }
    }
}
    




