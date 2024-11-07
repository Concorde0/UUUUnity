using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayStabAnimation : MonoBehaviour
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
            anim.Play("Stab", -1, 0.0f);
        }
    }
}
