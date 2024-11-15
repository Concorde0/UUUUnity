using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MagicBottle : MonoBehaviour
{
    public static MagicBottle instance;
    public Character character;
    public GameObject bottleSign;
    public float recoverTime;
    public float recoverCounter;
    public float magicBottleNumber;
    public bool isRecover;
    public UnityEvent<Character> Bottle;
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

    public void RecoverMagic()
    {
        if (bottleSign.activeSelf && !isRecover)
        {
            if (character.currentMagic >= 70)
            {
                character.currentMagic = 100;
            }
            else
            {
                character.currentMagic += magicBottleNumber;
            }
            isRecover = true;
        }
        Bottle?.Invoke(character);
        
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
