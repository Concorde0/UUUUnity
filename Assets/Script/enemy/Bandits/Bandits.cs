using System.Collections;
using System.Collections.Generic;
using Script.enemy.Bandits;
using UnityEngine;

public class Bandits : GroundEnemy
{
    private GroundEnemyBaseState banditsWaitState;
    private GroundEnemyBaseState banditsChaseState;
    private GroundEnemyBaseState banditsAttackState;
    private GroundEnemyBaseState banditsDeadState;
    public bool isChase;

    protected override void Awake()
    {
        base.Awake();
        banditsAttackState = new BanditsAttackState();
        banditsChaseState = new BanditsChaseState();
        banditsDeadState = new BanditsDeadState();
        banditsWaitState = new BanditsWaitState();

    }

    public override void OnEnable()
    {
        // GameManager.Instance.AddObserver(this);
        currentState = banditsWaitState;
        currentState.OnEnter(this);
        posEvent.OnEventRaised += OnposEvent;
        
    }
    public override void SwichState(NPCState state)
    {
        var newState = state switch
        {
            NPCState.Wait => banditsWaitState,
            NPCState.Chase => banditsChaseState,
            NPCState.Attack => banditsAttackState,
            NPCState.Dead =>  banditsDeadState,
            _ => null
        };
        
        currentState.OnExit();
        currentState = newState;
        currentState.OnEnter(this);
    }
    protected override void FixedUpdate()
    {
        if (!isHurt && !isDead && !wait && !attackPlayer && isChase )
        {
            Chase();
        }
        currentState.PhysicsUpdate();
    }
    private void Chase()
    {
        rb.velocity = new Vector2(currentSpeed * faceDir.x * Time.deltaTime, rb.velocity.y);
    }
}
