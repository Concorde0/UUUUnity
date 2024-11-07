using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.UI;

public class Bridge : MonoBehaviour
{
    public int health;
    private Rigidbody2D rb;
    public float invulunerableDuration;
    private float invulunerableCounter;
    public bool invulnerable;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
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
            //Debug.Log("change");
            rb.bodyType = RigidbodyType2D.Dynamic;
            
        }
        StartCoroutine(Fix());
    }
    private void TriggerInvulnerable()
    {
        if (!invulnerable)
        {
            invulnerable = true;
            invulunerableCounter = invulunerableDuration;
        }
    }
    private IEnumerator Fix()
    {
        yield return new WaitForSeconds(0.05f);
        //Debug.Log("change2");
        rb.gravityScale = 1;
        rb.freezeRotation = false;
    }
    

}
