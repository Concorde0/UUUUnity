using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.TextCore.Text;

public class Character : MonoBehaviour,ISaveable
{
    [Header("事件监听")]
    public VoidEventSO newGameEvent; 
    [Header("基础属性")]
    public float maxHealth;
    public float currentHealth;
    public float maxMagic;
    public float currentMagic;
    public float maxPower;
    public float currentPower;
    private Rigidbody2D rb;
    [Header("无敌")]
    public float invulunerableDuration;
    private float invulunerableCounter;
    public bool invulnerable;
    public UnityEvent<Rigidbody2D> characterRB;
    public UnityEvent<Character> PlayerTransfrom;
    public UnityEvent<Character> OnHealthChange;
    public UnityEvent<Transform> OnTakeDamage;
    public UnityEvent OnDie;
    private void NewGame()
    {
        //playerController.isHurt = false;
        currentHealth = maxHealth;
        OnHealthChange?.Invoke(this);
        

    }
    private void Awake()
    {
        //playerController = GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (invulnerable)
        {
            invulunerableCounter -= Time.deltaTime;
            if(invulunerableCounter <= 0)
            {
                invulnerable = false;
            }
        }
        //thisTransform = transform;
        
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Water"))
        {
            if (currentHealth > 0)
            {
                currentHealth = 0;
                OnHealthChange?.Invoke(this);
                OnDie?.Invoke();
            }
        }
    }

    private void OnEnable()
    {
        newGameEvent.OnEventRaised += NewGame;
        ISaveable saveable = this;
        saveable.RegisterSaveData();
        characterRB?.Invoke(rb);
        PlayerTransfrom?.Invoke(this);
    }

    private void OnDisable()
    {
        newGameEvent.OnEventRaised -= NewGame;
        ISaveable saveable = this;
        saveable.UnRegisterSaveData();
    }
    public void TakeDamage(Attack attacker)
    {
        if (invulnerable)
        {
            return;
        }
        if (currentHealth - attacker.damage > 0)
        {
            currentHealth -= attacker.damage;
            TriggerInvulnerable();
            OnTakeDamage?.Invoke(attacker.transform);
        }
        else
        {
            currentHealth = 0;
            OnDie?.Invoke();
        }
        characterRB?.Invoke(rb);
        OnHealthChange?.Invoke(this);
    }

    private void TriggerInvulnerable()
    {
        if (!invulnerable)
        {
            invulnerable = true;
            invulunerableCounter = invulunerableDuration;
        }
    }

    public DataDefination GetDataID()
    {
        return GetComponent<DataDefination>();
    }

    public void GetSaveData(Data data)
    {
        if (data.characterPosDict.ContainsKey(GetDataID().ID))
        {
            data.characterPosDict[GetDataID().ID] = transform.position;
            data.floatSaveData[GetDataID().ID + "health"]= this.currentHealth;
        }
        else
        {
            data.characterPosDict.Add(GetDataID().ID, transform.position);
            data.floatSaveData.Add(GetDataID().ID + "health", this.currentHealth);
        }
    }

    public void LoadData(Data data)
    {
        if (data.characterPosDict.ContainsKey(GetDataID().ID))
        {
            //transform.position = data.characterPosDict[GetDataID().ID];
            this.currentHealth = data.floatSaveData[GetDataID().ID + "health"];
            OnHealthChange?.Invoke(this);

        }
    }
}

   
