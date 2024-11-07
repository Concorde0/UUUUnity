using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class ChestMonster : MonoBehaviour, IInteractable
{
    private Animator anim;
    [Header("事件监听")]
    public PlayerControllerSO playerControllerEvent;
    private PlayerController herePlayerController;
    public CharacterEventSO posEvent;
    private float currentHealth;
    private float maxHealth;
    public bool isDone;
    private Character currentCharacter;
    public GameObject Coin;

    //if player dead , 广播给UI,再广播死亡面板
    public UnityEvent PlayerDead;
    public UnityEvent<Character> OnPlayerHealthChange;

    private void Awake()
    {
        
        anim = GetComponent<Animator>();
    }
    private void OnEnable()
    {
        posEvent.OnEventRaised += OnposEvent;
        playerControllerEvent.OnEventRaised += OnPlayerController;
    }
    private void OnDisable()
    {
        posEvent.OnEventRaised -= OnposEvent;
        playerControllerEvent.OnEventRaised -= OnPlayerController;
    }

    private void OnposEvent(Character character)
    {
        currentCharacter = character;
    }

    private void OnPlayerController(PlayerController playerController)
    {
        herePlayerController = playerController;
        
    }
    private void Update()
    {
        
    }

    public void TriggerAction()
    {
        if (!isDone)
        {
            OpenCheast();
        }
        
        //Debug.Log("Chest"+currentCharacter.currentHealth);
    }
    private void OpenCheast()
    {
        anim.SetTrigger("chewtrigger");
        isDone = true;
        this.gameObject.tag = "Untagged";
        if (herePlayerController != null)
        {
            herePlayerController.PlayerDead();
        }

        PlayerDead?.Invoke();
        currentCharacter.currentHealth = 0;
        OnPlayerHealthChange?.Invoke(currentCharacter);
    }
    

    public void Ondie()
    {
        gameObject.layer = 2;
        anim.SetBool("dead", true);
        //isDead = false;
        

    }
    public void DestroyAfterAnimation()
    {
        
        Instantiate(Coin, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }

   
}
