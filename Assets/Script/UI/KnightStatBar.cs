using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class KnightStatBar : MonoBehaviour
{
    public KnightStatBar knightStatBar;
    public Image healthImage;
    public Image healthDelayImage;
    [Header("ÊÂ¼þ¼àÌý")]
    public CharacterEventSO characterEvent;

    private void Update()
    {
        if (healthDelayImage.fillAmount > healthImage.fillAmount)
        {
            healthDelayImage.fillAmount -= Time.deltaTime;
        }
    }
    private void OnEnable()
    {
        characterEvent.OnEventRaised += OnKnightHealthEvent;
    }
    private void OnDisable()
    {
        characterEvent.OnEventRaised -= OnKnightHealthEvent;
    }

    private void OnKnightHealthEvent(Character character)
    {
        var presentage = character.currentHealth / character.maxHealth;
        knightStatBar.OnHealthChange(presentage);
    }

    public void OnHealthChange(float persentage)
    {
        healthImage.fillAmount = persentage;
    }
}
