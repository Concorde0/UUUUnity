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
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector]public Animator anim;
    [HideInInspector]public PhysiscCheck physiscCheck;
    [HideInInspector] public Knight knight;
    [HideInInspector] public Skeleton skeleton;
    [HideInInspector]public Seeker seeker;
    [HideInInspector] public Death death;
    [HideInInspector]public Character character;
    
    [Header("基本属性")]
    public float normalSpeed;
    public float chaseSpeed;
    public float attackDistance;
    public float hurtForce;
    public float currentSpeed;
    [HideInInspector] public Vector3 faceDir;
    [HideInInspector] public Transform attacker;

    [Header("事件监听")]
    public CharacterEventSO posEvent;
    [Header("物理检测")]
    public Vector2 centerOffset;
    public Vector2 checkSize;
    public float checkDistance;
    public LayerMask attackLayer;
    public Transform playerPos;
    
    [Header("Counter")]
    public bool wait;
    public float waitTime;
    public float waitTimeCounter;
    public float lostTime;
    public float lostTimeCounter;
    
    [Header("bool")]
    public bool isHurt;
    public bool isDead;
    public bool attackPlayer;
    public bool foundPlayer;
    [Header("prefab")]
    public GameObject bloodEffect;
    

    protected GroundEnemyBaseState currentState;
    protected GroundEnemyBaseState patrolState;
    protected GroundEnemyBaseState chaseState;
    
    protected virtual void Awake()
    {
        character = GetComponent<Character>();
        skeleton = GetComponent<Skeleton>();
        knight = GetComponent<Knight>();
        seeker = GetComponent<Seeker>();
        death = GetComponent<Death>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        physiscCheck = GetComponent<PhysiscCheck>();
        currentSpeed = normalSpeed;
        waitTimeCounter = waitTime; 
        
    }
    private void Start()
    {
        
    }

    public virtual void OnEnable()
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
    protected virtual void Update()
    {
        faceDir = new Vector3(-transform.localScale.x,0,0);
        currentState.LogicUpdate();
        TimeCounter();
    }

    protected virtual void FixedUpdate()
    {
        if (!isHurt && !isDead && !wait && !attackPlayer)
        {
            Move();
        }
        
        currentState.PhysicsUpdate();
    }


    protected void OnposEvent(Character character)
    {
        playerPos = character.transform;
    }

    protected virtual void Move()
    {
        rb.velocity = new Vector2 (currentSpeed * faceDir.x * Time.deltaTime,rb.velocity.y);
    }


    protected virtual void TimeCounter()
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
    


    public virtual void SwichState(NPCState state)
    {
        var newState = state switch
        {
            NPCState.Patrol => patrolState,
            NPCState.Chase => chaseState,

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

