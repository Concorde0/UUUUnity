using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SegaAttack2 : MonoBehaviour
{
    public GameObject attack2PlayerPosition;
    private GameObject player;
    private GameObject sega;
    private string targetTag = "Player";
    private string targetTag2 = "Sega";
    public bool isAttack2;
    public float verticalForce;
    public float horizontalForce;
    private Rigidbody2D playerRb;
    [Header("事件监听")]
    public RigidBodySO rbEvent;

    private void OnEnable()
    {
        rbEvent.OnEventRaised += OnRbEvent;
        
    }

    private void OnDisable()
    {
        rbEvent.OnEventRaised -= OnRbEvent;

    }

    private void OnRbEvent(Rigidbody2D playerRigidBody2D)
    {
        playerRb = playerRigidBody2D;
    }

    private void Update()
    {
        
    }

    private void FixedUpdate()
    {if (isAttack2)
        {
            sega = GameObject.FindGameObjectWithTag(targetTag2);
            player = GameObject.FindGameObjectWithTag(targetTag);
            player.transform.position = attack2PlayerPosition.transform.position;
            if (attack2PlayerPosition.activeSelf == false)
            {
                StartCoroutine(Fix());
                isAttack2 = false;
            }
        }
        
        
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isAttack2 = true;
        }
        
    }
    
    private IEnumerator Fix()
    {
        yield return new WaitForSeconds(0.05f);
        Vector2 forceDirection = new Vector2(-sega.transform.localScale.x * horizontalForce, verticalForce);
        playerRb.AddForce(forceDirection, ForceMode2D.Impulse);
    }
}
