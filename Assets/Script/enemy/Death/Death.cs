using System.Collections;
using System.Collections.Generic;
using Script.enemy.Death;
using UnityEngine;

public class Death : GroundEnemy
{
    private GroundEnemyBaseState deathChaseState;
    private GroundEnemyBaseState deathDeathState;
    private GroundEnemyBaseState deathAttackState;
    private GroundEnemyBaseState deathMagicState;
    private GroundEnemyBaseState deathHideState;
    private GroundEnemyBaseState deathWaitState;
    private GroundEnemyBaseState deathfireMagicState;
    [Header("bool")]
    public bool isChase;
    [Header("Distance")]
    public float hideDistance;
    [Header("Counter")] 
    public float magicCooldown = 5f;
    public float hideCooldown = 13f;
    public float fireCooldown = 7f;
    public float magicCooldownTimeCounter;
    public float fireCooldownTimeCounter;
    public float hideCooldownTimeCounter;

    [Header("Object")] 
    public GameObject magic;
    public GameObject fire;
    protected override void Awake()
    {
        base.Awake();
        deathChaseState = new DeathChaseState();
        deathAttackState = new DeathAttackState();
        deathMagicState = new DeathMagicState();
        deathfireMagicState = new DeathFireMagic();
        deathHideState = new DeathHideState();
        deathDeathState = new DeathDeadState();
        deathWaitState = new DeathWaitState();
        
    }

    public override void OnEnable()
    {
        currentState = deathWaitState;
        currentState.OnEnter(this);
        posEvent.OnEventRaised += OnposEvent;
        magicCooldownTimeCounter = magicCooldown;
        fireCooldownTimeCounter = fireCooldown;
    }

    protected override void Update()
    {
        base.Update();
        if (foundPlayer && hideCooldownTimeCounter >= 0)
        {
            hideCooldownTimeCounter -= Time.deltaTime;
        }
        if (foundPlayer && magicCooldownTimeCounter >= 0)
        {
            magicCooldownTimeCounter -= Time.deltaTime;
        }

        if (foundPlayer && fireCooldownTimeCounter >= 0)
        {
            fireCooldownTimeCounter -= Time.deltaTime;
        }
        
        
    }
    public override void SwichState(NPCState state)
    {
        var newState = state switch
        {
            NPCState.Wait => deathWaitState,
            NPCState.Chase => deathChaseState,
            NPCState.Attack => deathAttackState,
            NPCState.Magic => deathMagicState,
            NPCState.Magic1 => deathfireMagicState,
            NPCState.Hide => deathHideState,
            NPCState.Dead =>  deathDeathState,
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

    public void HideTransform()
    {
        transform.position = playerPos.position;
    }

    public void MagicStart()
    {
        magic.transform.position = new Vector3(playerPos.position.x, playerPos.position.y+5.5f, playerPos.position.z);
        magic.SetActive(true);
    }

    public void FireStart()
    {
        fire.SetActive(true);
    }

    
}
