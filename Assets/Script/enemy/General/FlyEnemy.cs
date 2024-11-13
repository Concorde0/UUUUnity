using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Drawing.Text;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.Processors;
using UnityEngine.Rendering;

public class FlyEnemy : MonoBehaviour
{
    public Rigidbody2D rb;
    [HideInInspector] public Animator anim;
    [HideInInspector] public PhysiscCheck physiscCheck;
    [HideInInspector] public PlayerController playerController;
    [HideInInspector] public Attack attack;
    [Header("事件监听")]
    public CharacterEventSO posEvent;
    [Header("基本属性")]
    public float normalSpeed;
    public float chaseSpeed;
    [HideInInspector] public float currentSpeed;
    [Header("飞行敌人")]
    public Transform movePos;
    public Transform leftDownPos;
    public Transform rightUpPos;
    public float radius;
    public float attackDistance;
    

    public Vector3 faceDir;
    [HideInInspector] public float hurtForce;
    [HideInInspector] public Transform attacker;
    [Header("检测图层")]
    public LayerMask attackLayer;
    [Header("Counter")]
    public float waitTime;
    public float waitTimeCounter;
    public bool wait;
    public float lostTime;
    public float lostTimeCounter;
    [Header("bool")]
    public bool isHurt;
    public bool isDead;
    public bool foundPlayer;
    public bool attackPlayer;
    public bool isBite;
    public bool isTail;
    [Header("其他")]
    public Transform playerPos;
    public GameObject bloodEffect;

    private FlyEnemyBaseState currentState;
    protected FlyEnemyBaseState patrolState;
    protected FlyEnemyBaseState hitState;
    protected FlyEnemyBaseState chaseState;
    protected FlyEnemyBaseState tailattackState;
   
    protected virtual void Awake()
    {

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        physiscCheck = GetComponent<PhysiscCheck>();
        currentSpeed = normalSpeed;
        waitTimeCounter = waitTime;
       
    }
    private void Start()
    {
        movePos.position = GetRandomPos();
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
        faceDir = new Vector3(-transform.localScale.x, 0, 0);
        currentState.LogicUpdate();
        TimeCounter();
        Distance();
        AttackDistance();
    }

    private void FixedUpdate()
    {
       
        if (!isHurt & !isDead && !wait && !foundPlayer && !isTail )
        {
            Move();
            
        }
        if (!isHurt & !isDead && !wait && foundPlayer && !isTail )
            Chase();
        currentState.PhysicsUpdate();
    }
    
    private void OnposEvent(Character character)
    {
        playerPos = character.transform;
    }
    public virtual void Distance()
    {
        float distance = (transform.position - playerPos.position).sqrMagnitude;
        if(distance <radius)
        {
            //Debug.Log("found");
            foundPlayer = true;
        }
    }
    public virtual void AttackDistance()
    {
        float distance = (transform.position - playerPos.position).sqrMagnitude;
        if (distance < attackDistance)
        {
            //Debug.Log("attack");
            attackPlayer = true;    
        }
    }

    protected virtual void Move()
    { 
        
        transform.position = Vector2.MoveTowards(transform.position, movePos.position, currentSpeed * Time.deltaTime);
    }
    protected virtual void Chase()
    {
        if (playerPos.localScale.x > 0)
        {
            Vector2 playerPosition = new Vector2(playerPos.position.x + 1, playerPos.position.y + 1);
            transform.position = Vector2.MoveTowards(transform.position, playerPosition, currentSpeed * Time.deltaTime);
        }
        else
        {
            Vector2 playerPosition = new Vector2(playerPos.position.x - 1, playerPos.position.y + 1);
            transform.position = Vector2.MoveTowards(transform.position, playerPosition, currentSpeed * Time.deltaTime);
        }
        
       
        
    }


    public Vector2 GetRandomPos()
    {
        //Debug.Log("randompos");
        Vector2 randomPos = new Vector2(Random.Range(leftDownPos.position.x, rightUpPos.position.x), Random.Range(leftDownPos.position.y, rightUpPos.position.y));
        return randomPos;
    }
    protected void TimeCounter()
    {
        if (wait)
        {
            //Debug.Log("11");
            waitTimeCounter -= Time.deltaTime;
            if (waitTimeCounter <= 0)
            {
                wait = false;
                waitTimeCounter = waitTime;
            }

        }

        if (lostTimeCounter > 0)
        {
            lostTimeCounter -= Time.deltaTime;
        }

    }

   


    public void SwichState(NPCState state)
    {
        var newState = state switch
        {
            NPCState.Patrol => patrolState,
            NPCState.Hit => hitState,
            NPCState.Chase => chaseState,
            NPCState.Tailattcak => tailattackState,
            _ => null
        };

        currentState.OnExit();
        currentState = newState;
        currentState.OnEnter(this);
    }

    public void OnTakeDamage(Transform attackTrans)
    {
        //Debug.Log("hurt");
        attacker = attackTrans;
        //turn
        if (attackTrans.position.x - transform.position.x > 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        if (attackTrans.position.x - transform.position.x < 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        //����

        isHurt = true;
        anim.SetTrigger("hurt");
        Vector2 dir = new Vector2(transform.position.x - attackTrans.position.x, 0).normalized;
        rb.velocity = new Vector2(0, rb.velocity.y);
        Instantiate(bloodEffect, transform.position, Quaternion.identity);
        StartCoroutine(OnHurt(dir));
    }

    private IEnumerator OnHurt(Vector2 dir)
    {
        rb.AddForce(dir * hurtForce, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.45f);
        isHurt = false;
    }

    public void OnDie()
    {
        //Debug.Log("Dead");
        gameObject.layer = 2;
        anim.SetBool("dead", true);
        isDead = false;

        StartCoroutine(OnDead());
    }
    public IEnumerator OnDead()
    {
        //Debug.Log("IE");
        yield return new WaitForSeconds(0.05f);
        anim.SetBool("dead", false);
    }

    public void DestroyAfterAnimation()
    {
        Destroy(this.gameObject);
    }
    //void OnDrawGizmosSelected()
    //{
    //    Gizmos.DrawWireSphere(radius.position, radius);
    //}

}

