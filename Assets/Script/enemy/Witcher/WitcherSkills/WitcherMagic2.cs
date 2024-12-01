using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitcherMagic2 : MonoBehaviour
{
    public RigidBodySO rbEvent;
    public CharacterEventSO characterEvent;
    private Rigidbody2D jumperRb;
    public GameObject magic2;
    public float upwardForceMagnitude;
    public float normalWardForceMagnitude;
    public float magnitudeDirection;
    public float directionFix;
   

    private void OnEnable()
    {
        rbEvent.OnEventRaised += OnrbEvent;
        characterEvent.OnEventRaised += OnCharacterEvent;
    }

    private void OnDisable()
    {
        rbEvent.OnEventRaised -= OnrbEvent;
        characterEvent.OnEventRaised -= OnCharacterEvent;

    }

    private void OnCharacterEvent(Character character)
    {
        magnitudeDirection = character.transform.position.x - transform.position.x;
        if (magnitudeDirection > 0)
        {
            directionFix = -1;
        }
        else
        {
            directionFix = 1;
        }
    }

    private void OnrbEvent(Rigidbody2D characterRigidbody)
    {
        jumperRb = characterRigidbody;
    }

    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Fix());
        }
    }

    private IEnumerator Fix()
    {
        yield return new WaitForSeconds(0.05f);
        Vector2 forceDirection = new Vector2( directionFix * normalWardForceMagnitude, upwardForceMagnitude);
        jumperRb.AddForce(forceDirection, ForceMode2D.Impulse);
        
    }

    private void OnDestroy()
    {
        magic2.SetActive(false);
    }
}
