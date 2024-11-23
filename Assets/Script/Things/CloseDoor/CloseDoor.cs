using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CloseDoor : MonoBehaviour
{
    private Animator anim;
    public GameObject witcher;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            anim.SetBool("door", true);
        }

        if (witcher.IsDestroyed())
        {
            anim.SetBool("door", false);
        }
    }
}
