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
    [Header("bool")]
    public bool isChase;
    [Header("Distance")]
    public float magicDistance;
    public float hideDistance;
    [Header("Counter")] 
    public float magicCooldown = 7f;
    public float hideCooldown = 13f;
    public float magicCooldownTimeCounter;
    public float hideCooldownTimeCounter;

    [Header("Object")] public GameObject magic;
    protected override void Awake()
    {
        base.Awake();
        deathChaseState = new DeathChaseState();
        deathAttackState = new DeathAttackState();
        deathMagicState = new DeathMagicState();
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
        
        
    }
    public override void SwichState(NPCState state)
    {
        var newState = state switch
        {
            NPCState.Wait => deathWaitState,
            NPCState.Chase => deathChaseState,
            NPCState.Attack => deathAttackState,
            NPCState.Magic => deathMagicState,
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

    
}
