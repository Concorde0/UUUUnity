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
    private CapsuleCollider2D cp;
    public bool hasbc;
    public bool hascp;
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
        
    }
    public void TakeDamage()
    {
        health -= 1;
        if (health < 0)
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
            StartCoroutine(Destory());
            Addforce();
            StartCoroutine(Fix());
        }
        
    }
    
    private IEnumerator Fix()
    {
        yield return new WaitForSeconds(0.05f);
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
    }

}
