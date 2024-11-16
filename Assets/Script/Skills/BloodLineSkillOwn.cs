using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodLineSkillOwn : MonoBehaviour
{
    private Animator anim;
    private PlayerInputControl inputControl;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        anim.SetBool("blood",true);
    }

    // anim.SetBool("blood",true);
    private void BloodLineSkillEnd()
    {
        anim.SetBool("blood",false);
        BloodLineSkill.instance.bloodLineSkill.SetActive(false);
    }
}
