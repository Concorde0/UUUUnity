using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buttons : MonoBehaviour,IInteractable
{
    private Animator anim;
    public bool isButton;
    public GameObject message;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void TriggerAction()
    {
        message.gameObject.SetActive(true);
        isButton = true;
        anim.SetTrigger("button");
        this.gameObject.tag = "Untagged";
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if(message != null && message ==true)
        {
            message.SetActive(false);
        } 
        
    }
}

// 