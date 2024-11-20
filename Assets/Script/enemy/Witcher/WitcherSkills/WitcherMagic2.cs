using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitcherMagic2 : MonoBehaviour
{
    public RigidBodySO rbEvent;
    public CharacterEventSO characterEvent;
    public float upwardForceMagnitude;
    public float normalWardForceMagnitude;
    public float playerLocalScale;
    private Rigidbody2D jumperRb;
    private void Awake()
    {
    }
    void Update()
    {
       
    }

    private void OnEnable()
    {
        Debug.Log("OnEnable");
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
        Debug.Log("playerLocalScale");
        playerLocalScale = character.transform.localScale.x;
    }

    private void OnrbEvent(Rigidbody2D characterRigidbody)
    {
        Debug.Log("jumperRb");
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
        yield return new WaitForSeconds(0.2f);
        Vector2 forceDirection = new Vector2( playerLocalScale * normalWardForceMagnitude, upwardForceMagnitude);
        jumperRb.AddForce(forceDirection, ForceMode2D.Impulse);
        
    }
}
