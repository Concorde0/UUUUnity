using System.Collections;
using System.Collections.Generic;
using Script.enemy.Sega;
using UnityEngine;

public class Sega : GroundEnemy
{
    public bool isChase;
    public float jumpAttackDistance;
    public float jumpAttackForce;
    public float jumpFlyAttackForce;
    public float magic1Cooldown = 5f;
    public float magic2Cooldown = 7f;
    public float jumpFlyAttackCooldown = 13f;
    public float magic1CooldownTimeCounter;
    public float magic2CooldownTimeCounter;
    public float jumpFlyAttackTimeCounter;
    
    public GameObject statBar;
    
    
    private GroundEnemyBaseState segaChaseState;
    private GroundEnemyBaseState segaAttackState;
    private GroundEnemyBaseState segaAttack2State;
    private GroundEnemyBaseState segaDeadState;
    private GroundEnemyBaseState segaMagic1State;
    private GroundEnemyBaseState segaMagic2State;
    private GroundEnemyBaseState segaJumpAttackState;
    private GroundEnemyBaseState segaJumpAttack2State;
    private GroundEnemyBaseState segaJumpFlyAttackState;
    private GroundEnemyBaseState segaEscapeState;
    protected override void Awake()
    {
        base.Awake();
        segaChaseState = new SegaChaseState();
        segaAttackState = new SegaAttackState();
        segaAttack2State = new SegaAttack2State();
        segaDeadState = new SegaDeadState();
        segaMagic1State = new SegaMagic1State();
        segaMagic2State = new SegaMagic2State();
        segaJumpAttackState = new SegaJumpAttackState();
        segaJumpFlyAttackState = new SegaJumpFlyAttackState();
        segaJumpFlyAttackState = new SegaJumpFlyAttack2State();
        segaEscapeState = new SegaEscapeState();

    }
    public override void OnEnable()
    {
        GameManager.Instance.AddObserver(this);
        currentState = segaChaseState;
        currentState.OnEnter(this);
        posEvent.OnEventRaised += OnposEvent;
    }
    protected override void Update()
    {
        base.Update();
        if (foundPlayer && jumpFlyAttackTimeCounter >= 0)
        {
            jumpFlyAttackTimeCounter -= Time.deltaTime;
        }
        if (foundPlayer && magic2CooldownTimeCounter>= 0)
        {
            magic2CooldownTimeCounter -= Time.deltaTime;
        }

        if (foundPlayer && magic1CooldownTimeCounter >= 0)
        {
            magic1CooldownTimeCounter -= Time.deltaTime;
        }
        
        
    }
    public override void SwichState(NPCState state)
    {
        var newState = state switch
        {
            NPCState.Chase => segaChaseState,
            NPCState.Attack => segaAttackState,
            NPCState.Attack2 => segaAttack2State,
            NPCState.Magic1 => segaMagic1State,
            NPCState.Magic2 => segaMagic2State,
            NPCState.JumpAttack => segaJumpAttackState,
            NPCState.JumpFlyAttack => segaJumpFlyAttackState,
            NPCState.JumpFlyAttack2 => segaJumpAttack2State,
            NPCState.Dead => segaDeadState,
            NPCState.Escape => segaEscapeState,
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
