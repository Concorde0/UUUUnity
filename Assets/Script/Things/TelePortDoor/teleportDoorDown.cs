using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teleportDoor : MonoBehaviour,IInteractable
{
    public Vector3 targetPosition;
    private Character currentCharacter;
    private static bool isTeleporting;
    private float teleportTime = 1f;
    private float teleportTimeCounter;
    [Header("事件监听")]
    public CharacterEventSO characterEvent;

   
    protected virtual void Update()
    {
        TimeCounter();
    }

    private void OnEnable()
    {
        characterEvent.OnEventRaised += OncharacterEvent;
    }

    private void OnDisable()
    {
        characterEvent.OnEventRaised -= OncharacterEvent;
    }

    private void OncharacterEvent(Character character)
    {
        currentCharacter = character;
    }
    public void TriggerAction()
    {
        if (!isTeleporting)
        {
            currentCharacter.transform.position = targetPosition;
            isTeleporting = true;
        }
        
    }
    protected virtual void TimeCounter()
    {
        if (isTeleporting)
        {
            teleportTimeCounter -= Time.deltaTime;
        }

        if (teleportTimeCounter <= 0)
        {
            isTeleporting = false;
            teleportTimeCounter = teleportTime;
        }
    }

    
}