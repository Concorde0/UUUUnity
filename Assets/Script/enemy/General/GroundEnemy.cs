using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Drawing.Text;
using System.Media;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.Processors;
using UnityEngine.Rendering;
using UnityEngine.TextCore.Text;

public class GroundEnemy : MonoBehaviour
{
    public Rigidbody2D rb;
    [HideInInspector]public Animator anim;
    [HideInInspector] public PhysiscCheck physiscCheck;
    [HideInInspector] public Knight knight;
    [Header("基本参数")]
    public float normalSpeed;
    public float chaseSpeed;
    public float attackDistance;
    public float foundDistance;
    [HideInInspector] public float currentSpeed;
    public Vector3 faceDir;
    [HideInInspector] public float hurtForce;
    [HideInInspector] public Transform attacker;

    [Header("监听事件")]
    public CharacterEventSO posEvent;
    [Header("检测")]
    public Vector2 centerOffset;
    public Vector2 checkSize;
    public float checkDistance;
    public LayerMask attackLayer;
    public Transform playerPos;
    public bool attackPlayer;
    public bool foundPlayer;



    [Header("计时器")]
    public float waitTime;
    public float waitTimeCounter;
    public bool wait;
    public float lostTime;
    public float lostTimeCounter;
    [Header("状态")]
    public bool isHurt;
    public bool isDead;
    [Header("特效")]
    public GameObject bloodEffect;


    protected GroundEnemyBaseState currentState;
    protected GroundEnemyBaseState patrolState;
    protected GroundEnemyBaseState chaseState;
    protected GroundEnemyBaseState attack1State;
    protected GroundEnemyBaseState attack2State;
    protected GroundEnemyBaseState idle2State;
    protected GroundEnemyBaseState stabState;
    protected GroundEnemyBaseState whackState;
    protected virtual void Awake()
    {
        
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        physiscCheck = GetComponent<PhysiscCheck>();
        currentSpeed = normalSpeed;
        waitTimeCounter = waitTime;
        knight = GetComponent<Knight>();    
        
    }
    private void Start()
    {
        
    }

    private void OnEnable()
    {
        posEvent.OnEventRaised += OnposEvent;
        currentState = patrolState;
        currentState.OnEnter(this);
    }
    private void OnDisable()
    {
        posEvent.OnEventRaised -= OnposEvent;
        currentState.OnExit();

    }
    private void Update()
    {
        faceDir = new Vector3(-transform.localScale.x,0,0);
        currentState.LogicUpdate();
        TimeCounter();
    }

    protected virtual void FixedUpdate()
    {
        if (!isHurt & !isDead && !wait )
        {
            Move();
        }
        
        currentState.PhysicsUpdate();
    }
    

    private void OnposEvent(Character character)
    {
        playerPos = character.transform;
    }
    //public virtual void Distance()
    //{
    //    float distance = (transform.position - playerPos.position).sqrMagnitude;
    //    if (distance < radius)
    //    {
    //        foundPlayer = true;
    //    }
    //}
    //public virtual void AttackDostamce()
    //{
    //    float distance = (transform.position - playerPos.position).sqrMagnitude;
    //    if (distance < attackDistance)
    //    {
    //        attackPlayer = true;
    //    }
    //}
    public virtual void Move()
    {
        rb.velocity = new Vector2 (currentSpeed * faceDir.x * Time.deltaTime,rb.velocity.y);
    }
   
    
    public virtual void TimeCounter()
    {
        if(wait)
        {
            waitTimeCounter -= Time.deltaTime;
            if(waitTimeCounter <= 0)
            {
                wait = false;
                waitTimeCounter = waitTime;
                transform.localScale = new Vector3(faceDir.x, transform.localScale.y, transform.localScale.z);
            }
            
        }

        if ( FoundPlayer() && lostTimeCounter>0)
        {
            lostTimeCounter -= Time.deltaTime;
        }
        
    }

    public virtual bool FoundPlayer()
    {
        return  Physics2D.BoxCast(transform.position + (Vector3)centerOffset, checkSize.normalized, 0, faceDir, checkDistance, attackLayer);
    }
    


    public void SwichState(NPCState state)
    {
        var newState = state switch
        {
            NPCState.Patrol => patrolState,
            NPCState.Chase => chaseState,
            NPCState.Attack1 => attack1State,
            NPCState.Attack2 => attack2State,
            NPCState.Idel2 => idle2State,
            NPCState.Stab => stabState,
            NPCState.Whack => whackState,

            _ => null
        };

        currentState.OnExit();
        currentState = newState;
        currentState.OnEnter(this);
    }

    public virtual void OnTakeDamage(Transform attackTrans)
    {
        
        attacker = attackTrans;
        //turn
        if(attackTrans.position.x - transform.position.x > 0)
        {
            transform.localScale = new Vector3(-transform.localScale.x,transform.localScale.y,transform.localScale.z);
        }
        if(attackTrans.position.x - transform.position.x < 0)
        {
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
        //击退
        
        isHurt = true;
        anim.SetTrigger("hurt");
        Vector2 dir = new Vector2(transform.position.x - attackTrans.position.x,0).normalized;
        rb.velocity = new Vector2(0, rb.velocity.y);
        Instantiate(bloodEffect, transform.position, Quaternion.identity);
        //Debug.Log("ins");
        StartCoroutine(OnHurt(dir));
    }

        private IEnumerator OnHurt(Vector2 dir)
        {
            rb.AddForce(dir * hurtForce, ForceMode2D.Impulse);
            yield return new WaitForSeconds(0.45f);
            isHurt = false;
        }

        public virtual void OnDie()
        {
            gameObject.layer = 2;
            anim.SetBool("dead", true);
            isDead = false;

            
        }
        

        public virtual void DestroyAfterAnimation()
        {
            Destroy(this.gameObject);
        }
    

        public virtual void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(transform.position + (Vector3)centerOffset + new Vector3(checkDistance* -transform.localScale.x,0), 0.2f);
        }
}

