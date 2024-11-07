using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAttack1Animation : MonoBehaviour
{
    private Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void OnEnable()
    {
        if (anim != null)
        {
            anim.Play("Attack1", -1, 0.0f);
        }
    }
}
