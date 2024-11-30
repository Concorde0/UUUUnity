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
    private GameObject target;
    private bool toPlayer;

    public float fixMoveTime = 0.1f;
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

    

    private void MoveToPlayer()
    {
        transform.position= Vector2.MoveTowards(transform.position, playerPos.position,smallDemonSpeed*Time.deltaTime);
    }

    private void MoveToSega()
    {
        transform.position= Vector2.MoveTowards(transform.position, target.transform.position ,smallDemonSpeed*Time.deltaTime);
    }

    private void FixMove()
    {
        
    }
    
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            toPlayer = true;
        }
    }

    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (toPlayer)
        {
            if (other.CompareTag("Sega"))
            {
                onHealthSega?.Invoke(this);
                Destroy(gameObject);
            }
        }
    }
}
