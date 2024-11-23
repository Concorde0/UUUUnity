using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public class HealthBottle : MonoBehaviour
{
    public static HealthBottle instance;
    public PlayerController playercontroller;
    public Character character;
    public GameObject healthEffect;
    public GameObject bottleSign;
    public float recoverTime;
    public float recoverCounter;
    public float healthBottleNumber;
    public bool isRecover;
    public UnityEvent<Character> Bottle;
    private void Awake()
    {
        instance = this;
        playercontroller = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
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
            if (Mathf.Approximately(character.currentHealth, 100))
            {
                return;
            }
            StartCoroutine(EffectTime());
            healthEffect.SetActive(true);
            playercontroller.isDrink = true;
            playercontroller.inputControl.GamePlay.Disable();
            if (character.currentHealth >= character.maxHealth-30f)
            {
                character.currentHealth = character.maxHealth;
            }
            else
            {
                character.currentHealth += healthBottleNumber;
            }
            isRecover = true;
        }
        Bottle?.Invoke(character);
        
    }
    private IEnumerator EffectTime()
    {
        yield return new WaitForSeconds(1.9f);
        healthEffect.SetActive(false);
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
