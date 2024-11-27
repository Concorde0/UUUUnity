using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XInput;

public class Sign : MonoBehaviour
{
    private PlayerInputControl playerInput;
    private Animator anim;
    public GameObject signSprite;
    public bool canPress;
    public Transform playerTrans;
    private IInteractable targetItem;
    private void Awake()
    {
        anim = signSprite.GetComponent<Animator>();
        playerInput = new PlayerInputControl();
        playerInput.Enable();
    }

    private void OnEnable()
    {
        InputSystem.onActionChange += OnActionChange;
        playerInput.GamePlay.Interact.started += OnConfirm;
    }

   

    private void Update()
    {
        signSprite.GetComponent<SpriteRenderer>().enabled = canPress;
        signSprite.transform.localScale = playerTrans.localScale;
    }
    private void OnConfirm(InputAction.CallbackContext context)
    {
        if (canPress)
        {
            targetItem.TriggerAction();
        }
    }
    private void OnActionChange(object obj, InputActionChange actionChange)
    {
        if(actionChange == InputActionChange.ActionStarted)
        {
            var d = (((InputAction)obj).activeControl.device);

            switch (d.device)
            {

                case Keyboard:
                    anim.Play("keyboard");
                    break;
                case XInputControllerWindows:
                    anim.Play("xbox");
                    break;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Interactable"))
        {
            canPress = true;
            targetItem = other.GetComponent<IInteractable>();
        }
        if (other.CompareTag("ChestMonster"))
        {
            canPress = true;
            targetItem = other.GetComponent<IInteractable>();
        }
        if (other.CompareTag("NPC"))
        {
            canPress = true;
            targetItem = other.GetComponent<IInteractable>();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        canPress = false;
    }
}
