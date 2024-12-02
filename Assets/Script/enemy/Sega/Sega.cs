using System.Collections;
using System.Collections.Generic;
using DG.Tweening.Plugins.Options;
using Script.enemy.Sega;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Sega : GroundEnemy
{
    
    public bool isChase;
    [Header("CoolDown")]
    public float magic1Cooldown = 5f;
    public float magic2Cooldown = 7f;
    public float jumpFlyAttackCooldown = 13f;
    public float attack2Cooldown = 10f;
    
    [Header("TimeCounter")]
    public float magic1CooldownTimeCounter;
    public float magic2CooldownTimeCounter;
    public float jumpFlyAttackTimeCounter;
    public float attack2CooldownTimeCounter;

    [Header("1")]
    // public float jumpFlyAttackVelocityY;
    public float jumpFlyAttackSpeed;

    public float jumpWallSpeed;
    public float wallMoveSpeed;
    public float jumpFlyAttackForce;
    public float jumpAttackDistance;
    public float jumpAttackForce;
    public float jumpAttackSpeed;

    [Header("bool")] 
    public bool magic1;
    public bool magic2;
    public bool jumpFlyAttack;
    public bool attack2;

    public bool isEscape;
    public bool isMoveWallPosition1;
    public bool isUp;
    

    [Header("position")] 
    public GameObject jumpPosition;
    public GameObject jumpWallPosition;
    public GameObject wallPosition1;
    public GameObject wallPosition2;
    public GameObject wallPositionUp;
    public GameObject waitPosition;
    


    [Header("事件监听")] 
    public VoidEventSO recoverHealth;


    public UnityEvent<Character> recover;
    
    
    
    public GameObject statBar;
    public GameObject rockPrefab;
    public GameObject magic2Prefab;
    public GameObject smallDemonPrefab;
    
    private GroundEnemyBaseState segaChaseState;
    private GroundEnemyBaseState segaAttackState;
    private GroundEnemyBaseState segaAttack2State;
    private GroundEnemyBaseState segaMagic1State;
    private GroundEnemyBaseState segaMagic2State;
    private GroundEnemyBaseState segaJumpAttackState;
    private GroundEnemyBaseState segaJumpAttack2State;
    private GroundEnemyBaseState segaJumpFlyAttack1State;
    private GroundEnemyBaseState segaJumpFlyAttack2State;
    private GroundEnemyBaseState segaEscapeState;
    private GroundEnemyBaseState segaJumpWallState;
    private GroundEnemyBaseState segaWallMoveState;
    private GroundEnemyBaseState segaWallWaitState;
    private GroundEnemyBaseState segaWallAttackState;
    private GroundEnemyBaseState segaWaitState;
    private GroundEnemyBaseState segaUpState;
    
    private GroundEnemyBaseState segaDeadState;
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
        segaJumpWallState = new SegaJumpWallState();
        segaWallMoveState = new SegaWallMoveState();
        segaWallAttackState = new SegaWallAttackState();
        segaWaitState = new SegaWaitState();
        
        segaDeadState = new SegaDeadState();
        physiscCheck = GetComponent<PhysiscCheck>();

    }
    public override void OnEnable()
    {
        GameManager.Instance.AddObserver(this);
        statBar.SetActive(true);
        currentState = segaChaseState;
        currentState.OnEnter(this);
        posEvent.OnEventRaised += OnposEvent;
        recoverHealth.OnEventRaised += OnRecoverHealth;
        magic1CooldownTimeCounter = magic1Cooldown;
        magic2CooldownTimeCounter = magic2Cooldown;
        attack2CooldownTimeCounter = attack2Cooldown;
        jumpFlyAttackTimeCounter = jumpFlyAttackCooldown;
    }

    public override void OnDisable()
    {
        base.OnDisable();
        recoverHealth.OnEventRaised -= OnRecoverHealth;

    }

    private void OnRecoverHealth()
    {
        Debug.Log("recoverEvent");
        if (character.currentHealth <= character.maxHealth - 20)
        {
            character.currentHealth += 20;
        }
        else
        {
            character.currentHealth = character.maxHealth;
        }
        
        recover?.Invoke(character);
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
            NPCState.Escape => segaEscapeState,
            NPCState.JumpWall => segaJumpWallState,
            NPCState.WallMove => segaWallMoveState,
            NPCState.WallAttack => segaWallAttackState,
            NPCState.SegaUp => segaUpState,
            NPCState.Wait => segaWaitState,
            NPCState.Dead => segaDeadState,
            
            _ => null
        };
        
        currentState.OnExit();
        currentState = newState;
        currentState.OnEnter(this);
    }
    protected override void FixedUpdate()
    {
        if (!isHurt && !isDead && !wait && !attackPlayer && isChase && !isEscape)
        {
            Chase();
        }

        if (isEscape)
        {
            Escape();
        }
        currentState.PhysicsUpdate();
    }

    private void Chase()
    {
        rb.velocity = new Vector2(currentSpeed * -faceDir.x * Time.deltaTime, rb.velocity.y);
    }

    private void Escape()
    {
        transform.position = Vector2.MoveTowards(transform.position, jumpPosition.transform.position,
            jumpWallSpeed * Time.deltaTime);
    }

    public void JumpToWallPosition()
    {
        transform.position = Vector2.MoveTowards(transform.position, jumpWallPosition.transform.position,
            jumpWallSpeed * Time.deltaTime);
    }

    public void MoveToWallPosition1()
    {
        transform.position = Vector2.MoveTowards(transform.position, wallPosition1.transform.position,
            wallMoveSpeed * Time.deltaTime);
    }

    public void MoveToWallPosition2()
    {
        transform.position = Vector2.MoveTowards(transform.position, wallPosition2.transform.position,
            wallMoveSpeed * Time.deltaTime);
    }
    public void MoveToWallPositionUp()
    {
        transform.position = Vector2.MoveTowards(transform.position, wallPositionUp.transform.position,
            wallMoveSpeed * Time.deltaTime);
    }
    
    public void WaitPosition()
    {
        transform.position = Vector2.MoveTowards(transform.position, waitPosition.transform.position,
            wallMoveSpeed * Time.deltaTime);
    }

    public void JumpFlyAttackTowards()
    {
        transform.position = Vector2.MoveTowards(transform.position,playerPos.position,jumpFlyAttackSpeed*Time.deltaTime);
        
    }

    public void InstantiateRock()
    {
        var playerPosition1 = new Vector2(playerPos.position.x+2, playerPos.position.y+1);
        var playerPosition2 = new Vector2(playerPos.position.x-2, playerPos.position.y+1);
        var playerPosition3 = new Vector2(playerPos.position.x, playerPos.position.y+6);
        Instantiate(rockPrefab, playerPosition1, Quaternion.identity);
        Instantiate(rockPrefab, playerPosition2, Quaternion.identity);
        Instantiate(magic2Prefab, playerPosition3, Quaternion.identity);
    }

    public void InstantiateSmallDemon()
    {
        var smallDemonPosition1 = new Vector2(transform.position.x+3, transform.position.y+5);
        var smallDemonPosition2 = new Vector2(transform.position.x-3, transform.position.y+5);
        Instantiate(smallDemonPrefab,smallDemonPosition1, Quaternion.identity);
        Instantiate(smallDemonPrefab,smallDemonPosition2, Quaternion.identity);
    }

    
}
