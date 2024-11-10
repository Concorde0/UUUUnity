using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideWall : MonoBehaviour
{
    public int health;
    public float invulunerableDuration;
    private float invulunerableCounter;
    public bool invulnerable;
    private Animator anim;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private void Start()
    {
        
    }
    private void Update()
    {
        if (invulnerable)
        {
            invulunerableCounter -= Time.deltaTime;
            if (invulunerableCounter <= 0)
            {
                invulnerable = false;
            }
        }
    }
    public void TakeDamage()
    {
        Debug.Log("bridge damage");
        if (invulnerable)
        {
            return;
        }
        health -= 1;
        TriggerInvulnerable();
        if (health < 0)
        {
            anim.SetBool("destory", true);
        }
    }
    private void TriggerInvulnerable()
    {
        if (!invulnerable)
        {
            invulnerable = true;
            invulunerableCounter = invulunerableDuration;

        }
    }
    public void DestoryWall()
    {
        Destroy(this.gameObject);
    }

    

} 
