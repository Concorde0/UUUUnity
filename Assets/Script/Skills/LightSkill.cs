using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class LightSkill : MonoBehaviour
{
    public static LightSkill instance;
    public new GameObject light;
    public GameObject lightSign;
    public float lightTime;
    public float lightTimeCounter;
    public bool isLight;
    public float magicConsume;
    public float magicConsumer;

    private void Awake()
    {
        instance = this;
        
    }

    private void OnEnable()
    {
        lightTimeCounter = lightTime;
    }
    

    private void Update()
    {
        LightTime();
        MagicConsume();
    }

    public void LightSkillStart()
    {
        if (!isLight)
        {
            light.SetActive(true);
            isLight = true;
            
        }
    }

    public void MagicConsume()
    {
        if (!isLight)
        {
            magicConsume = magicConsumer;
        }
        else
        {
            magicConsume = 0;
        }
    }

    private void LightTime()
    {
        if (isLight)
        {
            lightTimeCounter -= Time.deltaTime;
        }
        if (lightTimeCounter <= 0)
        {
            light.SetActive(false);
            isLight = false;
            lightTimeCounter = lightTime;
        }
    }

    
}
