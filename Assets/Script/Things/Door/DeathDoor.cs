using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DeathDoor : MonoBehaviour,IInteractable
{
    private Animator anim;
    private BoxCollider2D bx;
    public GameObject death;
    private bool isOpen;
    private void Awake()
    {
        bx = gameObject.GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
    }
    
    public void TriggerAction()
    {
        if (death.IsDestroyed() && !isOpen)
        {
            anim.SetBool("door",true);
            isOpen = true;
            bx.isTrigger = true;
            this.gameObject.tag = "Untagged";
        }
    }
}
