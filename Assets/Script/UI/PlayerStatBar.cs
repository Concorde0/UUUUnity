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
    public Image magicImage;
    public Image magicDelayImage;

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

        if (magicDelayImage.fillAmount > magicImage.fillAmount)
        {
            magicDelayImage.fillAmount -= Time.deltaTime;
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

    public void OnMagicChange(float percentage)
    {
        magicImage.fillAmount = percentage;
    }
}
