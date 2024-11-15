using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class WaterSkill : MonoBehaviour
{
    public static WaterSkill instance;
    public GameObject waterSkill;
    public GameObject waterSign;
    public bool isWaterSkill;
    public float magicConsume;
    public float magicConsumer;
   
    private void Awake()
    {
        MagicConsume();
        instance = this;

    }
    
    private void Update()
    {
        if (!waterSkill.activeSelf)
        {
            isWaterSkill = false;
        }
    }

    public void WaterSkillStart()
    {
        if (waterSign.activeSelf && !isWaterSkill)
        {
            waterSkill.SetActive(true);
        }
        isWaterSkill = true;
    }

    
    
    public void MagicConsume()
    {
        if (waterSign.activeSelf &&!isWaterSkill)
        {
            magicConsume = magicConsumer;
        }
        else
        {
            magicConsume = 0;
        }
    }

    
    
}
                    



