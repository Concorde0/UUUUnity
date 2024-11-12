using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering.UI;

public class Bridge : MonoBehaviour
{
    public int health;
    private Rigidbody2D rb;
    private BoxCollider2D bc;
    public bool hasbc;
    private CapsuleCollider2D cp;
    public bool hascp;
    public float invulunerableDuration;
    private float invulunerableCounter;
    public bool invulnerable;
    public float downwardForceMagnitude;
    public float otherwardForceMagnitude;
    private void Awake()
    {
        cp = GetComponent<CapsuleCollider2D>(); 
        bc = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        hasbc = (bc != null);
        hascp = (cp != null);
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
            StartCoroutine(Destory());
            Addforce();
            StartCoroutine(Fix());
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
    private IEnumerator Fix()
    {
        yield return new WaitForSeconds(0.05f);
        //Debug.Log("change2");
        rb.gravityScale = 1;
        rb.freezeRotation = false;
        yield return new WaitForSeconds(1f);
        if (hasbc)
        {
            bc.isTrigger = true;
        }
        if (hascp)
        {
            cp.isTrigger = true;
        }
        

    }
    private IEnumerator Destory()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }
    private void Addforce()
    {
        Vector2 forceDirection = new Vector2(otherwardForceMagnitude, downwardForceMagnitude);
        rb.AddForce(forceDirection, ForceMode2D.Impulse);
        Debug.Log("AddForce");
    }

}
