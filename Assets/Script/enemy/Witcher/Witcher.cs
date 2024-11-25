using System.Collections;
using System.Collections.Generic;
using Script.enemy.Witcher;
using UnityEngine;

public class Witcher : GroundEnemy
{
    public GroundEnemyBaseState WitcherWaitState;
    public GroundEnemyBaseState WitcherChaseState;
    public GroundEnemyBaseState WitcherMagic1State;
    public GroundEnemyBaseState WitcherMagic2State;
    public GroundEnemyBaseState WitcherMagic3State;
    public GroundEnemyBaseState WitcherDeadState;
    [Header("bool")]
    public bool isChase;
    public bool isMagic2;
    [Header("Float")]
    public float witcherMagic1Distance;
    public float witcherMagic2Distance;
    public float chaseDistance;
    public float witcherChaseSpeed;
    [Header("Cool Down")]
    public float magic1CoolDown ;
    public float magic2CoolDown ;
    public float magic3CoolDown ;
    public float magic1CoolDownCounter;
    public float magic2CoolDownCounter;
    public float magic3CoolDownCounter;
    [Header("Prefab")]
    public GameObject witcherMagic1Prefab;
    public GameObject witcherMagic2;
    public GameObject witcherMagic3Prefab;
    public GameObject statBar;
    
    
    
    protected override void Awake()
    {
        base.Awake();
        WitcherWaitState = new WitcherWaitState();
        WitcherChaseState = new WitcherChaseState();
        WitcherMagic1State = new WitcherMagic1State();
        WitcherMagic2State = new WitcherMagic2State();
        WitcherMagic3State = new WitcherMagic3State();
        WitcherDeadState = new WitcherDeadState();
        magic1CoolDownCounter = magic1CoolDown;
        magic2CoolDownCounter = magic2CoolDown;
        magic3CoolDownCounter = 0;
        
    }

    protected override void Update()
    {
        base.Update();
        CoolDownCounter();
    }

    public override void OnEnable()
    {
        GameManager.Instance.AddObserver(this);
        currentState = WitcherWaitState;
        currentState.OnEnter(this);
        posEvent.OnEventRaised += OnposEvent;
    }
    public override void SwichState(NPCState state)
    {
        var newState = state switch
        {
           NPCState.Wait => WitcherWaitState, 
           NPCState.Chase => WitcherChaseState,
           NPCState.Magic1 => WitcherMagic1State,
           NPCState.Magic2 => WitcherMagic2State,
           NPCState.Magic3 => WitcherMagic3State,
           NPCState.Dead => WitcherDeadState,
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

    private void CoolDownCounter()
    {
        if (foundPlayer)
        {
            magic1CoolDownCounter -= Time.deltaTime;
            magic2CoolDownCounter -= Time.deltaTime;
            magic3CoolDownCounter -= Time.deltaTime;
        }
    }

    public void Magic1()
    {
        Instantiate(witcherMagic1Prefab, transform.position, Quaternion.identity);
    }
    
    public void Magic2()
    {
        witcherMagic2.SetActive(true);
    }
    public void Magic3()
    {
        Vector3 magic2Position = new Vector3(transform.position.x+6, transform.position.y-3, transform.position.z);
        // Vector3 magic3Position = new Vector3(transform.position.x-2, transform.position.y-3, transform.position.z);
        Instantiate(witcherMagic3Prefab, magic2Position, Quaternion.identity);
        // Instantiate(witcherMagic3Prefab, magic3Position, Quaternion.identity);
    }
}
