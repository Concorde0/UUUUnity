using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MagicBottle : MonoBehaviour
{
    public static MagicBottle instance;
    public Character character;
    public PlayerController playercontroller;
    public GameObject bottleSign;
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
            playercontroller.isDrink = true;
            playercontroller.inputControl.GamePlay.Disable();
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
