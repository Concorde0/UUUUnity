using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private Animator anim;
    private Buttons buttonScript;
    private bool isDoor;
    private bool doorIsButton;
    private Transform buttonTransform;
    private BoxCollider2D bc;
    public void Start()
    {
        bc = gameObject.GetComponent<BoxCollider2D>();
        buttonTransform = transform.GetChild(0);
        buttonScript = buttonTransform.GetComponent<Buttons>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {

        if (!isDoor)
        {
            doorIsButton = buttonScript.isButton;
            OpenDoor();
        }
            
    }

    private void OpenDoor()
    {
        if (doorIsButton)
        {
            anim.SetBool("door",true);
            isDoor = true;
            bc.isTrigger = true;
        }
    }
}
