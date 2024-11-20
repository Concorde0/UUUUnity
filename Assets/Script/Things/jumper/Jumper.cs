using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Jumper : MonoBehaviour
{
    public RigidBodySO rbEvent;
    public float upwardForceMagnitude;
    private GameObject playerObject;
    private Rigidbody2D jumperRb;
    private Animator anim;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    void Update()
    {
       
    }

    private void OnEnable()
    {
        rbEvent.OnEventRaised += OnrbEvent;
    }

    private void OnDisable()
    {
        rbEvent.OnEventRaised -= OnrbEvent;
    }

    private void OnrbEvent(Rigidbody2D characterRigidbody)
    {
        jumperRb = characterRigidbody;
    }

    

    private void OnTriggerStay2D(Collider2D other)
    {
        Vector2 forceDirection = new Vector2(0, upwardForceMagnitude);
        if (other.gameObject.CompareTag("Player"))
        {
            anim.SetTrigger("isjumper");
            jumperRb.AddForce(forceDirection, ForceMode2D.Impulse);
        }
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {

        
    }
    
}
    

