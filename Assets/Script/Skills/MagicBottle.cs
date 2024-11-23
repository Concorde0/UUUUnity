using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MagicBottle : MonoBehaviour
{
    public static MagicBottle instance;
    public Character character;
    public GameObject bottleSign;
    public GameObject magicEffect;
    public PlayerController playercontroller;
    public float recoverTime;
    public float recoverCounter;
    public float magicBottleNumber;
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

    public void RecoverMagic()
    {
        if (bottleSign.activeSelf && !isRecover)
        {
            if (Mathf.Approximately(character.currentMagic, 100))
            {
                return;
            }
            StartCoroutine(EffectTime());
            magicEffect.SetActive(true);
            playercontroller.isDrink = true;
            playercontroller.inputControl.GamePlay.Disable();
            if (character.currentMagic >= character.maxMagic-30f)
            {
                character.currentMagic = character.maxMagic;
            }
            else
            {
                character.currentMagic += magicBottleNumber;
            }
            isRecover = true;
        }
        Bottle?.Invoke(character);
        
    }

    private IEnumerator EffectTime()
    {
        yield return new WaitForSeconds(1.9f);
        magicEffect.SetActive(false);
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
