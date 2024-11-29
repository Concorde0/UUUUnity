using System.Collections;
using System.Collections.Generic;
using DG.Tweening.Plugins.Options;
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
    public float attack2Cooldown = 10f;
    public float magic1CooldownTimeCounter;
    public float magic2CooldownTimeCounter;
    public float jumpFlyAttackTimeCounter;
    public float attack2CooldownTimeCounter;
    
    public float jumpAttackSpeed;
    
    public GameObject statBar;
    private PhysiscCheck physiscCheck;
    
    
    private GroundEnemyBaseState segaChaseState;
    private GroundEnemyBaseState segaAttackState;
    private GroundEnemyBaseState segaAttack2State;
    private GroundEnemyBaseState segaDeadState;
    private GroundEnemyBaseState segaMagic1State;
    private GroundEnemyBaseState segaMagic2State;
    private GroundEnemyBaseState segaJumpAttackState;
    private GroundEnemyBaseState segaJumpAttack2State;
    private GroundEnemyBaseState segaJumpFlyAttack1State;
    private GroundEnemyBaseState segaJumpFlyAttack2State;
    private GroundEnemyBaseState segaEscapeState;
    protected override void Awake()
    {
        base.Awake();
        segaChaseState = new SegaChaseState();
        segaAttackState = new SegaAttackState();
        segaAttack2State = new SegaAttack2State();
        segaMagic1State = new SegaMagic1State();
        segaMagic2State = new SegaMagic2State();
        segaJumpAttackState = new SegaJumpAttackState();
        segaJumpAttack2State = new SegaJumpAttack2State();
        segaJumpFlyAttack1State = new SegaJumpFlyAttackState();
        segaJumpFlyAttack2State = new SegaJumpFlyAttack2State();
        segaEscapeState = new SegaEscapeState();
        segaDeadState = new SegaDeadState();

        physiscCheck = GetComponent<PhysiscCheck>();

    }
    public override void OnEnable()
    {
        GameManager.Instance.AddObserver(this);
        currentState = segaChaseState;
        currentState.OnEnter(this);
        posEvent.OnEventRaised += OnposEvent;
        
        magic1CooldownTimeCounter = magic1Cooldown;
        magic2CooldownTimeCounter = magic2Cooldown;
        attack2CooldownTimeCounter = attack2Cooldown;
        jumpFlyAttackTimeCounter = jumpFlyAttackCooldown;
    }
    protected override void Update()
    {
        base.Update();
        if (jumpFlyAttackTimeCounter >= 0)
        {
            jumpFlyAttackTimeCounter -= Time.deltaTime;
        }
        if ( magic2CooldownTimeCounter>= 0)
        {
            magic2CooldownTimeCounter -= Time.deltaTime;
        }

        if (magic1CooldownTimeCounter >= 0)
        {
            magic1CooldownTimeCounter -= Time.deltaTime;
        }

        if ( attack2CooldownTimeCounter >= 0)
        {
            attack2CooldownTimeCounter -= Time.deltaTime;
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
            NPCState.JumpAttack2 => segaJumpAttack2State,
            NPCState.JumpFlyAttack => segaJumpFlyAttack1State,
            NPCState.JumpFlyAttack2 => segaJumpFlyAttack2State,
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
    public void Chase()
    {
        rb.velocity = new Vector2(currentSpeed * -faceDir.x * Time.deltaTime, rb.velocity.y);
    }
}
