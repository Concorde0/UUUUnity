using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class SmallDemon : GroundEnemy
{
    public float smallDemonSpeed;
    private string targetTag = "Sega";
    private string targetTag2 = "Player";
    private GameObject target;
    private GameObject target2;
    private bool toPlayer;
    
    private float fixMoveTime = 1f;
    private float fixMoveTimeCounter;
        

    public UnityEvent<SmallDemon> onHealthSega;

   

    public override void OnEnable()
    {
        fixMoveTimeCounter = fixMoveTime;
        posEvent.OnEventRaised += OnposEvent;
        GameManager.Instance.AddObserver(this);
    }

    public override void OnDisable()
    {
        GameManager.Instance.RemoveObserver(this);
        posEvent.OnEventRaised -= OnposEvent;
    }

    protected override void Update()
    {
        target2 = GameObject.FindGameObjectWithTag(targetTag2);
        if (playerPos == null)
        {
            Debug.Log("null");
        }

        if (fixMoveTimeCounter >= 0)
        {
            fixMoveTimeCounter -= Time.deltaTime;
        }
        
        if (!toPlayer && fixMoveTimeCounter <=0)
        {
            MoveToPlayer();
        }
        
        if(toPlayer)
        {
            target = GameObject.FindGameObjectWithTag(targetTag);
            MoveToSega();
        }
        DestroySelf();
    }
    public override void SwichState(NPCState state)
    {
        var newState = state switch
        {
            NPCState.Patrol => patrolState,
            NPCState.Chase => chaseState,

            _ => null
        };
        
    }
    protected override void Move()
    {
    }
    
    protected override void FixedUpdate()
    {
        
    }
    public override void OnDie()
    {
        gameObject.layer = 2;
        anim.SetTrigger("hurt");
        isDead = false;
    }

    

    private void MoveToPlayer()
    {
        transform.position= Vector2.MoveTowards(transform.position, target2.transform.position,smallDemonSpeed*Time.deltaTime);
    }

    private void MoveToSega()
    {
        Vector2 toSega = new Vector2(target.transform.position.x, target.transform.position.y+2);
        transform.position= Vector2.MoveTowards(transform.position, toSega ,smallDemonSpeed*Time.deltaTime);
    }

    private void FixMove()
    {
        
    }
    
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            
            StartCoroutine(FixCollider());
        }
        
    }

    private IEnumerator FixCollider()
    {
        yield return new WaitForSeconds(0.1f);
        toPlayer = true;
    }
    
    private void DestroySelf()
    {
        if (toPlayer)
        {
            if (Vector2.Distance(transform.position, target.transform.position) <= 2.3f)
            {
                onHealthSega?.Invoke(this);
                Destroy(gameObject);
            }
        }
    }

    
   
}
