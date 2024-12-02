using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsSegaBoss : MonoBehaviour,IInteractable
{
    public GameObject hideRoad;
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void TriggerAction()
    {  
        anim.SetTrigger("button");
        hideRoad.SetActive(true);
        this.gameObject.tag = "Untagged";
    }
}
