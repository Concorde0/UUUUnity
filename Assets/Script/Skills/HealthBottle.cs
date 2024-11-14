using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthBottle : MonoBehaviour
{
    public static HealthBottle instance;
    public Character character;
    public GameObject bottleSign;
    public float recoverTime;
    public float recoverCounter;
    public float healthBottleNumber;
    public bool isRecover;
    private void Awake()
    {
        instance = this;
    }

    private void OnEnable()
    {
        recoverCounter = recoverTime;
    }

    private void Update()
    {
        RecoverTime();
    }

    public void RecoverHealth()
    {
        if (bottleSign.activeSelf && !isRecover)
        {
            if (character.currentHealth >= 70)
            {
                character.currentHealth = 100;
            }
            else
            {
                character.currentHealth += healthBottleNumber;
            }
            isRecover = true;
        }
    }

    private void RecoverTime()
    {
        if (isRecover)
        {
            recoverCounter -= Time.deltaTime;
        }
        if (recoverCounter <= 0)
        {
            isRecover = false;
            recoverCounter = recoverTime;
        }
    }
    
    
}
