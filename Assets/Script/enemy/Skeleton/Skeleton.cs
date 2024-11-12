using DG.Tweening.Plugins.Options;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Skeleton : GroundEnemy
{
    public GroundEnemyBaseState attackState;
    public GroundEnemyBaseState reBornState;
    public Transform LeftPositon;
    public Transform RightPositon;
    public Transform movePos;
    public bool isChase;
    [Header("Check")]

    [Header("Reborn Conter")]
    public float rebornTime;
    public float rebornTimeConter;
    protected override void Awake()
    {
        base.Awake();
        rebornTimeConter = rebornTime;
        movePos = LeftPositon;
        patrolState = new SkeletonPatrolState();
        chaseState = new SkeletonChaseState();
        attackState = new SkeletonAttackState();
        reBornState = new SkeletonRebornState();
    }
    protected override void FixedUpdate()
    {
        if (!isHurt && !isDead && !wait && !attackPlayer && !isChase)
        {
            Move();
        }
        if(!isHurt && !isDead && !wait && !attackPlayer && isChase)
        {
            Debug.Log("ChaseSpeed");
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
            NPCState.Rebone => reBornState,

            _ => null
        };
        currentState.OnExit();
        currentState = newState;
        currentState.OnEnter(this);
    }

    protected void Reborn()
    {
        
        anim.SetBool("reborn", true);
        rebornTimeConter -= Time.deltaTime;
        if(rebornTimeConter < 0)
        {
            anim.SetBool("reborn", false);
            character.currentHealth = character.maxHealth;
            //anim.SetBool("compose",true);
        }

    }

    public override void OnDie()
    {
        //gameObject.layer = 2;
        //anim.SetBool("dead", true);
        //isDead = false;
    }
    public override void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position, movePos.position, currentSpeed * Time.deltaTime);
    }
    public void Chase()
    {
        rb.velocity = new Vector2(currentSpeed * -faceDir.x * Time.deltaTime, rb.velocity.y);
    }
   

    public override void TimeCounter()
    {
        if (wait)
        {
            waitTimeCounter -= Time.deltaTime;
            if (waitTimeCounter <= 0)
            {
                wait = false;
                waitTimeCounter = waitTime;
            }

        }
    }
}




