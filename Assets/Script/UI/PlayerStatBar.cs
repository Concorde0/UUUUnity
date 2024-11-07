using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatBar : MonoBehaviour
{
    public Image healthImage;
    public Image healthDelayImage;
    public Image powerImage;
    public Image powerDelayImage;

    private void Update()
    {
        if(healthDelayImage.fillAmount > healthImage.fillAmount)
        {
            healthDelayImage.fillAmount -= Time.deltaTime;
        }
        if (powerDelayImage.fillAmount > powerImage.fillAmount)
        {
            powerDelayImage.fillAmount -= Time.deltaTime;
        }
    }

    public void OnHealthChange(float percentage)
    {
        healthImage.fillAmount = percentage;
    }
    public void OnPowerChange(float percentage)
    {
        powerImage.fillAmount = percentage;
    }
}
