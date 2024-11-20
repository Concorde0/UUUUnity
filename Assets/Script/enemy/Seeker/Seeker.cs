using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seeker : GroundEnemy
{
    private GroundEnemyBaseState seekerSpawnState;
    private GroundEnemyBaseState seekerChaseState;
    private GroundEnemyBaseState seekerAttackState;
    private GroundEnemyBaseState seekerDeadState;
    public bool isChase;
    public bool isFind;
    protected override void Awake()
    {
        base.Awake();
        seekerSpawnState = new SeekerSpawnState();
        seekerChaseState = new SeekerChaseState();
        seekerAttackState = new SeekerAttackState();
        seekerDeadState = new SeekerDeadState();
    }
    public override void OnEnable()
    {
        posEvent.OnEventRaised += OnposEvent;
        currentState = seekerSpawnState;
        currentState.OnEnter(this);
    }

    public override void SwichState(NPCState state)
    {
        var newState = state switch
        {
            NPCState.Spawn => seekerSpawnState,
            NPCState.Chase => seekerChaseState,
            NPCState.Attack => seekerAttackState,
            NPCState.Dead =>  seekerDeadState,
            _ => null
        };
        
        currentState.OnExit();
        currentState = newState;
        currentState.OnEnter(this);
    }
    protected override void FixedUpdate()
    {
        if (!isHurt && !isDead && !wait && !attackPlayer && isChase)
        {
            Chase();
        }
        currentState.PhysicsUpdate();
    }
    private void Chase()
    {
        rb.velocity = new Vector2(currentSpeed * -faceDir.x * Time.deltaTime, rb.velocity.y);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        
        if (other.CompareTag("Player"))
        {
            isFind = true;
        }
    }
}
