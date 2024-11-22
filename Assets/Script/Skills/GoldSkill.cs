using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldSkill : MonoBehaviour
{
    public static GoldSkill instance;
    public GameObject goldSign;
    public GameObject goldSkill;

    public bool isGoldSkill;    
    public float magicConsume;
    public float magicConsumer;
    private void Awake()
    {
        instance = this;
        
    }
    private void Update()
    {
        MagicConsume();
        if (!goldSkill.activeSelf)
        {
            isGoldSkill = false;
        }
    }

    public void GoldSkillStart()
    {
        if (!isGoldSkill)
        {
            goldSkill.SetActive(true);
        }
        isGoldSkill = true;
        
    }
    private void MagicConsume()
    {
        if (!isGoldSkill )
        {
            magicConsume = magicConsumer;
        }
        else
        {
            magicConsume = 0;
        }
    }
    
}
