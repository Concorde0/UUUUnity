using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodLineSkill : MonoBehaviour
{
    public static BloodLineSkill instance;
    public GameObject bloodLineSkill;
    public GameObject bloodLineSign;
    public bool isBloodLine;
    public float magicConsume;
    public float magicConsumer;

    private void Awake()
    {
        MagicConsume();
        instance = this;
    }

    private void Update()
    {
        if (!bloodLineSkill.activeSelf)
        {
            
            isBloodLine = false;
        }
    }

    public void BloodLineSkillStart()
    {
        if (!isBloodLine)
        {
            
            bloodLineSkill.SetActive(true);
        }
        isBloodLine = true;
    }

    private void MagicConsume()
    {
        if (!isBloodLine )
        {
            magicConsume = magicConsumer;
        }
        else
        {
            magicConsume = 0;
        }
    }
}
